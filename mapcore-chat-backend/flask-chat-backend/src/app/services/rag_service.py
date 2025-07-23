import os
import chromadb
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_community.document_loaders import DirectoryLoader, TextLoader
from langchain_google_genai import GoogleGenerativeAIEmbeddings
import google.generativeai as genai

class RAGService:
    def __init__(self, folder_path):
        print(f"[RAG] Initializing RAG service with folder: {folder_path}")
        self.folder_path = folder_path
        
        if not os.path.exists(folder_path):
            print(f"[RAG] ERROR: Folder does not exist: {folder_path}")
            return
            
        print(f"[RAG] Creating ChromaDB client...")
        self.client = chromadb.Client()
        self.collection = self.client.create_collection("documents")
        
        print(f"[RAG] Initializing Google embeddings...")
        self.embeddings = GoogleGenerativeAIEmbeddings(model="models/embedding-001")
        
        print(f"[RAG] Loading documents...")
        self.load_documents()
    
    def load_documents(self):
        """Load and index documents from the specified folder"""
        try:
            print(f"[RAG] Scanning folder: {self.folder_path}")
            
            # List all files in the folder first
            all_files = []
            supported_files = []
            for root, dirs, files in os.walk(self.folder_path):
                for file in files:
                    file_path = os.path.join(root, file)
                    all_files.append(file_path)
                    
                    # Extended support for programming languages
                    if file.lower().endswith((
                        # C language files
                        '.c', '.h', '.cpp', '.hpp', '.cc', '.cxx',
                        # C# files
                        '.cs', '.csx', '.cshtml', '.razor',
                        # React/JavaScript/TypeScript files
                        '.js', '.jsx', '.ts', '.tsx', '.json',
                        # Web files
                        '.html', '.htm', '.css', '.scss', '.sass',
                        # Configuration files
                        '.xml', '.xaml', '.config', '.settings',
                        # Documentation
                        '.txt', '.md', '.markdown', '.rst',
                        # Project files
                        '.csproj', '.sln', '.vcxproj', '.props', '.targets',
                        # Package files
                        'package.json', 'package-lock.json', '.npmrc',
                        # Other useful files
                        '.yaml', '.yml', '.ini', '.conf'
                    )):
                        supported_files.append(file_path)
            
            print(f"[RAG] Found {len(all_files)} total files")
            print(f"[RAG] Found {len(supported_files)} supported code files")
            
            # Show breakdown by file type
            file_types = {}
            for file_path in supported_files:
                ext = os.path.splitext(file_path)[1].lower()
                file_types[ext] = file_types.get(ext, 0) + 1
            
            print(f"[RAG] File types breakdown: {file_types}")
            
            if not supported_files:
                print("[RAG] No supported code files found!")
                print("[RAG] Supported extensions: .c, .h, .cs, .js, .jsx, .ts, .tsx, .html, .css, etc.")
                return
            
            # Load each supported file individually
            documents = []
            failed_files = []
            
            for file_path in supported_files:
                try:
                    print(f"[RAG] Loading file: {os.path.basename(file_path)}")
                    
                    # Try multiple encodings for different file types
                    encodings_to_try = ['utf-8', 'utf-8-sig', 'latin1', 'cp1252']
                    
                    for encoding in encodings_to_try:
                        try:
                            loader = TextLoader(file_path, encoding=encoding)
                            file_docs = loader.load()
                            
                            # Add file type metadata
                            for doc in file_docs:
                                doc.metadata['file_type'] = os.path.splitext(file_path)[1]
                                doc.metadata['file_name'] = os.path.basename(file_path)
                                doc.metadata['language'] = self._detect_language(file_path)
                            
                            documents.extend(file_docs)
                            print(f"[RAG] ✓ Successfully loaded: {os.path.basename(file_path)} ({encoding})")
                            break
                            
                        except UnicodeDecodeError:
                            continue
                    else:
                        failed_files.append(file_path)
                        print(f"[RAG] ✗ Failed to load: {os.path.basename(file_path)} (encoding issues)")
                        
                except Exception as e:
                    failed_files.append(file_path)
                    print(f"[RAG] ✗ Failed to load {os.path.basename(file_path)}: {e}")
                    continue
            
            print(f"[RAG] Successfully loaded {len(documents)} documents")
            print(f"[RAG] Failed to load {len(failed_files)} files")
            
            if not documents:
                print("[RAG] WARNING: No documents were loaded!")
                return
            
            # Print first document sample
            if documents:
                print(f"[RAG] Sample document content: {documents[0].page_content[:200]}...")
            
            # Use code-aware text splitter
            text_splitter = RecursiveCharacterTextSplitter(
                chunk_size=1500,  # Larger chunks for code
                chunk_overlap=300,
                separators=[
                    "\n\n",  # Double newlines
                    "\nclass ",  # Class definitions
                    "\nfunction ",  # Function definitions
                    "\npublic ",  # C# public methods
                    "\nprivate ",  # C# private methods
                    "\n// ",  # Comments
                    "\n/* ",  # Block comments
                    "\n",  # Single newlines
                    " ",  # Spaces
                    ""
                ]
            )
            chunks = text_splitter.split_documents(documents)
            print(f"[RAG] Split into {len(chunks)} chunks")
            
            # Create embeddings and store in vector DB
            for i, chunk in enumerate(chunks):
                if i % 10 == 0:  # Progress update every 10 chunks
                    print(f"[RAG] Processing chunk {i+1}/{len(chunks)}")
                
                embedding = self.embeddings.embed_query(chunk.page_content)
                
                self.collection.add(
                    embeddings=[embedding],
                    documents=[chunk.page_content],
                    metadatas=[{
                        "source": chunk.metadata.get("source", ""),
                        "file_type": chunk.metadata.get("file_type", ""),
                        "file_name": chunk.metadata.get("file_name", ""),
                        "language": chunk.metadata.get("language", "")
                    }],
                    ids=[str(i)]
                )
            print(f"[RAG] Successfully indexed {len(chunks)} code chunks")
            
        except Exception as e:
            print(f"[RAG] Error loading documents: {e}")
            import traceback
            traceback.print_exc()
    
    def _detect_language(self, file_path):
        """Detect programming language from file extension"""
        ext = os.path.splitext(file_path)[1].lower()
        language_map = {
            '.c': 'C',
            '.h': 'C',
            '.cpp': 'C++',
            '.hpp': 'C++',
            '.cc': 'C++',
            '.cxx': 'C++',
            '.cs': 'C#',
            '.csx': 'C#',
            '.js': 'JavaScript',
            '.jsx': 'React/JavaScript',
            '.ts': 'TypeScript',
            '.tsx': 'React/TypeScript',
            '.html': 'HTML',
            '.css': 'CSS',
            '.scss': 'SCSS',
            '.xml': 'XML',
            '.xaml': 'XAML',
            '.json': 'JSON'
        }
        return language_map.get(ext, 'Unknown')
    
    def search_documents(self, query, n_results=5):  # Increased results for code
        """Search for relevant documents"""
        try:
            print(f"[RAG] Searching for: '{query}'")
            query_embedding = self.embeddings.embed_query(query)
            
            results = self.collection.query(
                query_embeddings=[query_embedding],
                n_results=n_results
            )
            
            found_docs = results['documents'][0] if results['documents'] else []
            found_metadata = results['metadatas'][0] if results['metadatas'] else []
            
            print(f"[RAG] Found {len(found_docs)} relevant code snippets")
            
            # Print what languages/files were found
            if found_metadata:
                languages = [meta.get('language', 'Unknown') for meta in found_metadata]
                files = [meta.get('file_name', 'Unknown') for meta in found_metadata]
                print(f"[RAG] Languages found: {set(languages)}")
                print(f"[RAG] Files found: {set(files)}")
            
            return found_docs
        except Exception as e:
            print(f"[RAG] Error searching documents: {e}")
            return []
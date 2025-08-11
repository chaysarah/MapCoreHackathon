import os
import json
import pickle
import hashlib
import chromadb
from chromadb.config import Settings
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_community.document_loaders import TextLoader, PyPDFLoader, Docx2txtLoader, UnstructuredWordDocumentLoader
from langchain_google_genai import GoogleGenerativeAIEmbeddings
import google.generativeai as genai
from concurrent.futures import ThreadPoolExecutor, as_completed
import gc
import threading

class RAGService:
    def __init__(self, folder_path):
        print(f"[RAG] Initializing RAG service with folder: {folder_path}")
        self.folder_path = folder_path
        self.db_path = os.path.join(os.path.dirname(folder_path), "chroma_db")
        self.cache_file = os.path.join(os.path.dirname(folder_path), "rag_cache.json")
        
        if not os.path.exists(folder_path):
            print(f"[RAG] ERROR: Folder does not exist: {folder_path}")
            return
        
        # Create persistent ChromaDB client with compatibility for different versions
        print(f"[RAG] Creating persistent ChromaDB client at: {self.db_path}")
        try:
            # Try with basic optimized settings
            self.client = chromadb.PersistentClient(
                path=self.db_path,
                settings=Settings(
                    anonymized_telemetry=False
                )
            )
            print(f"[RAG] ChromaDB client created successfully")
        except Exception as e:
            print(f"[RAG] Error creating ChromaDB client: {e}")
            # Fallback to simplest configuration
            try:
                self.client = chromadb.PersistentClient(path=self.db_path)
                print(f"[RAG] ChromaDB client created with minimal settings")
            except Exception as e2:
                print(f"[RAG] Critical error: Cannot create ChromaDB client: {e2}")
                raise e2
        
        print(f"[RAG] Initializing Google embeddings...")
        self.embeddings = GoogleGenerativeAIEmbeddings(model="models/embedding-001")
        
        # Check if we need to reload documents
        if self._should_reload_documents():
            print(f"[RAG] Documents changed, reloading...")
            self._cleanup_existing_collection()
            # Create collection with optimized settings for large datasets
            try:
                self.collection = self.client.create_collection(
                    name="documents",
                    metadata={"hnsw:space": "cosine", "hnsw:construction_ef": 200, "hnsw:M": 16}
                )
            except Exception as e:
                print(f"[RAG] Failed to create collection with advanced metadata: {e}")
                print(f"[RAG] Creating collection with basic settings...")
                self.collection = self.client.create_collection(name="documents")
            self.load_documents()
            self._save_cache()
        else:
            print(f"[RAG] Loading existing collection...")
            try:
                self.collection = self.client.get_collection("documents")
                print(f"[RAG] Loaded existing collection with {self.collection.count()} chunks")
                # Note: Incremental updates are handled in _should_reload_documents()
            except Exception:
                print(f"[RAG] No existing collection found, creating new one...")
                try:
                    self.collection = self.client.create_collection(
                        name="documents",
                        metadata={"hnsw:space": "cosine", "hnsw:construction_ef": 200, "hnsw:M": 16}
                    )
                except Exception as e:
                    print(f"[RAG] Failed to create collection with advanced metadata: {e}")
                    print(f"[RAG] Creating collection with basic settings...")
                    self.collection = self.client.create_collection(name="documents")
                self.load_documents()
                self._save_cache()
    
    def _get_folder_hash(self):
        """Generate hash of all files in folder to detect changes"""
        hasher = hashlib.md5()
        
        for root, dirs, files in os.walk(self.folder_path):
            for file in sorted(files):  # Sort for consistent hashing
                if file.lower().endswith((
                    '.c', '.h', '.cpp', '.hpp', '.cc', '.cxx',
                    '.cs', '.csx', '.cshtml', '.razor',
                    '.js', '.jsx', '.ts', '.tsx', '.json',
                    '.html', '.htm', '.css', '.scss', '.sass',
                    '.xml', '.xaml', '.config', '.settings',
                    '.txt', '.md', '.markdown', '.rst',
                    '.csproj', '.sln', '.vcxproj', '.props', '.targets',
                    'package.json', 'package-lock.json', '.npmrc',
                    '.yaml', '.yml', '.ini', '.conf',
                    # Document formats
                    '.pdf', '.doc', '.docx'
                )):
                    file_path = os.path.join(root, file)
                    try:
                        # Hash file path and modification time
                        hasher.update(file_path.encode())
                        hasher.update(str(os.path.getmtime(file_path)).encode())
                    except:
                        continue
        
        return hasher.hexdigest()
    
    def _should_reload_documents(self):
        """Check if documents have changed since last load - now supports incremental updates"""
        if not os.path.exists(self.cache_file):
            return True
        
        try:
            with open(self.cache_file, 'r') as f:
                cache_data = json.load(f)
            
            # Check if we can do incremental update
            if 'indexed_files' in cache_data:
                return self._should_incremental_update(cache_data)
            else:
                # Old cache format, do full reload
                current_hash = self._get_folder_hash()
                return cache_data.get('folder_hash') != current_hash
        except:
            return True
    
    def _should_incremental_update(self, cache_data):
        """Determine if incremental update is needed and perform it"""
        cached_files = cache_data.get('indexed_files', {})
        current_files = self._get_indexed_files_info()
        
        # Find new, modified, and deleted files
        new_files = []
        modified_files = []
        deleted_files = []
        
        # Check for new and modified files
        for file_path, file_info in current_files.items():
            if file_path not in cached_files:
                new_files.append(file_path)
            elif (cached_files[file_path]['mtime'] != file_info['mtime'] or 
                  cached_files[file_path]['size'] != file_info['size']):
                modified_files.append(file_path)
        
        # Check for deleted files
        for file_path in cached_files:
            if file_path not in current_files:
                deleted_files.append(file_path)
        
        total_changes = len(new_files) + len(modified_files) + len(deleted_files)
        
        if total_changes == 0:
            print("[RAG] No file changes detected, using existing index")
            return False
        
        print(f"[RAG] Detected changes: {len(new_files)} new, {len(modified_files)} modified, {len(deleted_files)} deleted files")
        
        # If too many changes (>20% of files), do full rebuild
        total_files = len(current_files)
        if total_changes > total_files * 0.2:
            print(f"[RAG] Too many changes ({total_changes}/{total_files}), doing full rebuild")
            return True
        
        # Perform incremental update
        try:
            self._perform_incremental_update(new_files, modified_files, deleted_files)
            return False  # Don't do full reload
        except Exception as e:
            print(f"[RAG] Incremental update failed: {e}, falling back to full rebuild")
            return True
    
    def _save_cache(self):
        """Save cache metadata"""
        cache_data = {
            'folder_hash': self._get_folder_hash(),
            'last_updated': str(os.path.getmtime(self.folder_path)),
            'collection_count': self.collection.count() if hasattr(self, 'collection') else 0,
            'indexed_files': self._get_indexed_files_info()
        }
        
        with open(self.cache_file, 'w') as f:
            json.dump(cache_data, f)
        
        print(f"[RAG] Cache saved with {cache_data['collection_count']} documents")
    
    def _get_indexed_files_info(self):
        """Get information about currently indexed files"""
        files_info = {}
        for root, dirs, files in os.walk(self.folder_path):
            for file in files:
                if file.lower().endswith((
                    '.c', '.h', '.cpp', '.hpp', '.cc', '.cxx',
                    '.cs', '.csx', '.cshtml', '.razor',
                    '.js', '.jsx', '.ts', '.tsx', '.json',
                    '.html', '.htm', '.css', '.scss', '.sass',
                    '.xml', '.xaml', '.config', '.settings',
                    '.txt', '.md', '.markdown', '.rst',
                    '.csproj', '.sln', '.vcxproj', '.props', '.targets',
                    'package.json', 'package-lock.json', '.npmrc',
                    '.yaml', '.yml', '.ini', '.conf',
                    # Document formats
                    '.pdf', '.doc', '.docx'
                )):
                    file_path = os.path.join(root, file)
                    try:
                        files_info[file_path] = {
                            'mtime': os.path.getmtime(file_path),
                            'size': os.path.getsize(file_path)
                        }
                    except:
                        continue
        return files_info
    
    def _load_file_parallel(self, file_path):
        """Load a single file with multiple encoding attempts - for parallel processing"""
        try:
            file_ext = os.path.splitext(file_path)[1].lower()
            
            # Handle different file types
            if file_ext == '.pdf':
                return self._load_pdf_file(file_path)
            elif file_ext == '.docx':
                return self._load_docx_file(file_path)
            elif file_ext == '.doc':
                return self._load_doc_file(file_path)
            else:
                # Handle text-based files
                return self._load_text_file(file_path)
                
        except Exception as e:
            return None, str(e)
    
    def _load_text_file(self, file_path):
        """Load text-based files with encoding detection"""
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
                    doc.metadata['category'] = self._classify_file_type(file_path)
                    doc.metadata['priority'] = self._get_file_priority(file_path)
                
                return file_docs, None
                
            except UnicodeDecodeError:
                continue
        else:
            return None, "encoding issues"
    
    def _load_pdf_file(self, file_path):
        """Load PDF files"""
        try:
            loader = PyPDFLoader(file_path)
            file_docs = loader.load()
            
            # Add file type metadata
            for doc in file_docs:
                doc.metadata['file_type'] = '.pdf'
                doc.metadata['file_name'] = os.path.basename(file_path)
                doc.metadata['language'] = 'PDF Document'
                doc.metadata['category'] = self._classify_file_type(file_path)
                doc.metadata['priority'] = self._get_file_priority(file_path)
            
            return file_docs, None
        except Exception as e:
            return None, f"PDF loading error: {str(e)}"
    
    def _load_docx_file(self, file_path):
        """Load DOCX files"""
        try:
            loader = Docx2txtLoader(file_path)
            file_docs = loader.load()
            
            # Add file type metadata
            for doc in file_docs:
                doc.metadata['file_type'] = '.docx'
                doc.metadata['file_name'] = os.path.basename(file_path)
                doc.metadata['language'] = 'Word Document'
                doc.metadata['category'] = self._classify_file_type(file_path)
                doc.metadata['priority'] = self._get_file_priority(file_path)
            
            return file_docs, None
        except Exception as e:
            return None, f"DOCX loading error: {str(e)}"
    
    def _load_doc_file(self, file_path):
        """Load legacy DOC files with fallback options"""
        try:
            # Try UnstructuredWordDocumentLoader first (requires LibreOffice)
            loader = UnstructuredWordDocumentLoader(file_path)
            file_docs = loader.load()
            
            # Add file type metadata
            for doc in file_docs:
                doc.metadata['file_type'] = '.doc'
                doc.metadata['file_name'] = os.path.basename(file_path)
                doc.metadata['language'] = 'Word Document (Legacy)'
                doc.metadata['category'] = self._classify_file_type(file_path)
                doc.metadata['priority'] = self._get_file_priority(file_path)
            
            return file_docs, None
            
        except Exception as e:
            error_msg = str(e).lower()
            if 'soffice' in error_msg or 'libreoffice' in error_msg:
                # LibreOffice not installed, try alternative approach
                print(f"[RAG] LibreOffice not found, trying alternative method for {os.path.basename(file_path)}")
                return self._load_doc_file_alternative(file_path)
            else:
                return None, f"DOC loading error: {str(e)}"
    
    def _load_doc_file_alternative(self, file_path):
        """Alternative method to load DOC files without LibreOffice"""
        try:
            # Try using docx2txt which can sometimes handle .doc files
            import docx2txt
            text_content = docx2txt.process(file_path)
            
            if text_content and text_content.strip():
                from langchain.schema import Document as LangChainDocument
                file_doc = LangChainDocument(
                    page_content=text_content,
                    metadata={
                        'source': file_path,
                        'file_type': '.doc',
                        'file_name': os.path.basename(file_path),
                        'language': 'Word Document (Legacy)'
                    }
                )
                return [file_doc], None
            else:
                return None, "No text content extracted from DOC file"
                
        except ImportError:
            # docx2txt not available, provide helpful guidance
            return None, "Legacy DOC file requires LibreOffice or docx2txt. Please install LibreOffice, convert to DOCX format, or install docx2txt package."
                
        except Exception as e:
            # DOC files are binary format, python-docx won't work
            error_msg = str(e).lower()
            if "word/document.xml" in error_msg or "archive" in error_msg:
                return None, f"Legacy DOC file format not supported by current libraries. Please convert '{os.path.basename(file_path)}' to DOCX format or install LibreOffice for processing."
            else:
                return None, f"Failed to process DOC file: {str(e)}"

    def _perform_incremental_update(self, new_files, modified_files, deleted_files):
        """Perform incremental update of the vector database"""
        print(f"[RAG] Performing incremental update...")
        
        # Handle deleted files first
        if deleted_files:
            print(f"[RAG] Removing {len(deleted_files)} deleted files from index")
            for file_path in deleted_files:
                file_name = os.path.basename(file_path)
                try:
                    # Get all chunks from this file and delete them
                    results = self.collection.get(
                        where={"file_name": file_name}
                    )
                    if results['ids']:
                        self.collection.delete(ids=results['ids'])
                        print(f"[RAG] ✓ Removed {len(results['ids'])} chunks from {file_name}")
                except Exception as e:
                    print(f"[RAG] ✗ Failed to remove {file_name}: {e}")
        
        # Handle new and modified files
        files_to_process = new_files + modified_files
        if files_to_process:
            print(f"[RAG] Processing {len(files_to_process)} new/modified files")
            
            # For modified files, remove old chunks first
            for file_path in modified_files:
                file_name = os.path.basename(file_path)
                try:
                    results = self.collection.get(
                        where={"file_name": file_name}
                    )
                    if results['ids']:
                        self.collection.delete(ids=results['ids'])
                        print(f"[RAG] ✓ Removed old chunks from modified file: {file_name}")
                except Exception as e:
                    print(f"[RAG] ✗ Failed to remove old chunks from {file_name}: {e}")
            
            # Load and index new/modified files
            documents = []
            failed_files = []
            
            # Use parallel loading for new/modified files
            max_workers = min(4, len(files_to_process))
            with ThreadPoolExecutor(max_workers=max_workers) as executor:
                future_to_file = {
                    executor.submit(self._load_file_parallel, file_path): file_path 
                    for file_path in files_to_process
                }
                
                for future in as_completed(future_to_file):
                    file_path = future_to_file[future]
                    try:
                        file_docs, error = future.result()
                        if file_docs:
                            documents.extend(file_docs)
                            print(f"[RAG] ✓ Loaded: {os.path.basename(file_path)}")
                        else:
                            failed_files.append(file_path)
                            print(f"[RAG] ✗ Failed to load: {os.path.basename(file_path)} ({error})")
                    except Exception as e:
                        failed_files.append(file_path)
                        print(f"[RAG] ✗ Failed to load {os.path.basename(file_path)}: {e}")
            
            if documents:
                # Split documents into chunks
                text_splitter = RecursiveCharacterTextSplitter(
                    chunk_size=1000,
                    chunk_overlap=200,
                    separators=[
                        "\n\n", "\nclass ", "\nfunction ", "\ndef ",
                        "\npublic ", "\nprivate ", "\nprotected ", "\nstatic ",
                        "\n// ", "\n/* ", "\n", " ", ""
                    ]
                )
                chunks = text_splitter.split_documents(documents)
                print(f"[RAG] Created {len(chunks)} new chunks")
                
                # Get current max ID to avoid conflicts
                try:
                    existing_results = self.collection.get()
                    if existing_results['ids']:
                        max_id = max(int(id_) for id_ in existing_results['ids'] if id_.isdigit())
                    else:
                        max_id = -1
                except:
                    max_id = -1
                
                # Add new chunks with batch processing
                batch_size = 50  # Smaller batches for incremental updates
                for i in range(0, len(chunks), batch_size):
                    batch = chunks[i:i + batch_size]
                    batch_embeddings = []
                    batch_documents = []
                    batch_metadatas = []
                    batch_ids = []
                    
                    for j, chunk in enumerate(batch):
                        embedding = self.embeddings.embed_query(chunk.page_content)
                        batch_embeddings.append(embedding)
                        batch_documents.append(chunk.page_content)
                        batch_metadatas.append({
                            "source": chunk.metadata.get("source", ""),
                            "file_type": chunk.metadata.get("file_type", ""),
                            "file_name": chunk.metadata.get("file_name", ""),
                            "language": chunk.metadata.get("language", ""),
                            "category": chunk.metadata.get("category", "Core"),
                            "priority": chunk.metadata.get("priority", 5)
                        })
                        batch_ids.append(str(max_id + i + j + 1))
                    
                    self.collection.add(
                        embeddings=batch_embeddings,
                        documents=batch_documents,
                        metadatas=batch_metadatas,
                        ids=batch_ids
                    )
                    
                    del batch_embeddings, batch_documents, batch_metadatas, batch_ids
                    gc.collect()
                
                print(f"[RAG] Successfully added {len(chunks)} new chunks to index")
        
        # Update cache with new file information
        self._save_cache()
        print(f"[RAG] Incremental update completed successfully")

    def _cleanup_existing_collection(self):
        """Remove existing collection if it exists"""
        try:
            self.client.delete_collection("documents")
            print(f"[RAG] Cleaned up existing collection")
        except:
            pass
    
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
                    
                    # Extended support for programming languages and documents
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
                        '.yaml', '.yml', '.ini', '.conf',
                        # Document formats
                        '.pdf', '.doc', '.docx'
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
                print("[RAG] Supported extensions: .c, .h, .cs, .js, .jsx, .ts, .tsx, .html, .css, .pdf, .doc, .docx, etc.")
                return
            
            # Load files in parallel for better performance
            documents = []
            failed_files = []
            
            print(f"[RAG] Loading {len(supported_files)} files in parallel...")
            
            # Use ThreadPoolExecutor for parallel file loading
            max_workers = min(8, len(supported_files))  # Limit concurrent threads
            with ThreadPoolExecutor(max_workers=max_workers) as executor:
                # Submit all file loading tasks
                future_to_file = {
                    executor.submit(self._load_file_parallel, file_path): file_path 
                    for file_path in supported_files
                }
                
                # Process completed tasks
                for future in as_completed(future_to_file):
                    file_path = future_to_file[future]
                    try:
                        file_docs, error = future.result()
                        if file_docs:
                            documents.extend(file_docs)
                            print(f"[RAG] ✓ Successfully loaded: {os.path.basename(file_path)}")
                        else:
                            failed_files.append(file_path)
                            print(f"[RAG] ✗ Failed to load: {os.path.basename(file_path)} ({error})")
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
            
            # Use optimized code-aware text splitter for large datasets
            text_splitter = RecursiveCharacterTextSplitter(
                chunk_size=1000,  # Smaller chunks for better granularity and faster processing
                chunk_overlap=200,  # Reduced overlap
                separators=[
                    "\n\n",  # Double newlines
                    "\nclass ",  # Class definitions
                    "\nfunction ",  # Function definitions
                    "\ndef ",  # Python function definitions
                    "\npublic ",  # C# public methods
                    "\nprivate ",  # C# private methods
                    "\nprotected ",  # Protected methods
                    "\nstatic ",  # Static methods
                    "\n// ",  # Comments
                    "\n/* ",  # Block comments
                    "\n",  # Single newlines
                    " ",  # Spaces
                    ""
                ]
            )
            chunks = text_splitter.split_documents(documents)
            print(f"[RAG] Split into {len(chunks)} chunks")
            
            # Create embeddings and store in vector DB with batch processing
            batch_size = 100  # Process in batches for better memory management
            total_chunks = len(chunks)
            
            for i in range(0, total_chunks, batch_size):
                batch = chunks[i:i + batch_size]
                batch_embeddings = []
                batch_documents = []
                batch_metadatas = []
                batch_ids = []
                
                print(f"[RAG] Processing batch {i//batch_size + 1}/{(total_chunks + batch_size - 1)//batch_size} ({len(batch)} chunks)")
                
                # Process batch
                for j, chunk in enumerate(batch):
                    embedding = self.embeddings.embed_query(chunk.page_content)
                    batch_embeddings.append(embedding)
                    batch_documents.append(chunk.page_content)
                    batch_metadatas.append({
                        "source": chunk.metadata.get("source", ""),
                        "file_type": chunk.metadata.get("file_type", ""),
                        "file_name": chunk.metadata.get("file_name", ""),
                        "language": chunk.metadata.get("language", ""),
                        "category": chunk.metadata.get("category", "Core"),
                        "priority": chunk.metadata.get("priority", 5)
                    })
                    batch_ids.append(str(i + j))
                
                # Add batch to collection
                self.collection.add(
                    embeddings=batch_embeddings,
                    documents=batch_documents,
                    metadatas=batch_metadatas,
                    ids=batch_ids
                )
                
                # Clear memory after each batch
                del batch_embeddings, batch_documents, batch_metadatas, batch_ids
                gc.collect()
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
            '.json': 'JSON',
            '.pdf': 'PDF Document',
            '.doc': 'Word Document (Legacy)',
            '.docx': 'Word Document'
        }
        return language_map.get(ext, 'Unknown')
    
    def _classify_file_type(self, file_path):
        """Classify file as API, example, documentation, or other"""
        file_name = os.path.basename(file_path).lower()
        dir_path = os.path.dirname(file_path).lower()
        
        # API files - high priority
        api_indicators = [
            'api', 'interface', 'service', 'controller', 'endpoint',
            'rest', 'graphql', 'webapi', 'client', 'sdk'
        ]
        
        # Example files - lower priority
        example_indicators = [
            'example', 'sample', 'demo', 'test', 'tutorial', 
            'playground', 'sandbox', 'hello'
        ]
        
        # Documentation files - medium priority
        doc_indicators = [
            'readme', 'doc', 'guide', 'manual', 'specification',
            'spec', 'reference', 'help'
        ]
        
        # Check file name and directory path
        full_path = f"{dir_path}/{file_name}"
        
        # Priority order: API > Documentation > Examples > Other
        if any(indicator in full_path for indicator in api_indicators):
            return 'API'
        elif any(indicator in full_path for indicator in doc_indicators):
            return 'Documentation'
        elif any(indicator in full_path for indicator in example_indicators):
            return 'Example'
        else:
            return 'Core'  # Regular code files
    
    def _get_file_priority(self, file_path):
        """Get numerical priority for file types (higher = more important)"""
        category = self._classify_file_type(file_path)
        priority_map = {
            'API': 10,          # Highest priority
            'Documentation': 7,  # High priority
            'Core': 5,          # Medium priority  
            'Example': 3        # Lower priority
        }
        return priority_map.get(category, 5)
    
    def search_documents(self, query, n_results=5, filter_metadata=None, prioritize_api=True):
        """Search for relevant documents with optional metadata filtering and API prioritization"""
        try:
            print(f"[RAG] Searching for: '{query}' (prioritize_api={prioritize_api})")
            query_embedding = self.embeddings.embed_query(query)
            
            if prioritize_api:
                # First search for API files specifically
                api_results = self._search_by_category(query_embedding, 'API', n_results // 2)
                
                # Then search for other high-priority files
                other_results = self._search_general(query_embedding, n_results - len(api_results[0]), 
                                                   exclude_category='API', filter_metadata=filter_metadata)
                
                # Combine results with API files first
                combined_docs = api_results[0] + other_results[0]
                combined_metadata = api_results[1] + other_results[1]
                
                # Truncate to requested number
                found_docs = combined_docs[:n_results]
                found_metadata = combined_metadata[:n_results]
                
            else:
                # Regular search without prioritization
                found_docs, found_metadata = self._search_general(query_embedding, n_results, 
                                                                filter_metadata=filter_metadata)
            
            print(f"[RAG] Found {len(found_docs)} relevant code snippets")
            
            # Print what categories/files were found
            if found_metadata:
                categories = [meta.get('category', 'Unknown') for meta in found_metadata]
                priorities = [meta.get('priority', 0) for meta in found_metadata]
                files = [meta.get('file_name', 'Unknown') for meta in found_metadata]
                
                # Count categories
                category_counts = {}
                for cat in categories:
                    category_counts[cat] = category_counts.get(cat, 0) + 1
                
                print(f"[RAG] Categories found: {category_counts}")
                print(f"[RAG] Average priority: {sum(priorities)/len(priorities):.1f}")
                print(f"[RAG] Files found: {set(files)}")
            
            return found_docs, found_metadata
            
        except Exception as e:
            print(f"[RAG] Error searching documents: {e}")
            return [], []
    
    def _search_by_category(self, query_embedding, category, n_results):
        """Search specifically within a category"""
        try:
            results = self.collection.query(
                query_embeddings=[query_embedding],
                n_results=min(n_results * 2, 50),  # Get more to filter
                where={"category": category}
            )
            
            found_docs = results['documents'][0] if results['documents'] else []
            found_metadata = results['metadatas'][0] if results['metadatas'] else []
            
            # Limit to requested number
            return found_docs[:n_results], found_metadata[:n_results]
            
        except Exception as e:
            print(f"[RAG] Error in category search: {e}")
            return [], []
    
    def _search_general(self, query_embedding, n_results, exclude_category=None, filter_metadata=None):
        """General search with optional category exclusion"""
        try:
            # Build query parameters
            query_params = {
                "query_embeddings": [query_embedding],
                "n_results": min(n_results * 2, 50)  # Get more to filter
            }
            
            # Add metadata filtering
            where_clause = {}
            if exclude_category:
                where_clause["category"] = {"$ne": exclude_category}
            if filter_metadata:
                where_clause.update(filter_metadata)
            
            if where_clause:
                query_params["where"] = where_clause
            
            results = self.collection.query(**query_params)
            
            found_docs = results['documents'][0] if results['documents'] else []
            found_metadata = results['metadatas'][0] if results['metadatas'] else []
            
            # Sort by priority if available
            if found_metadata:
                doc_meta_pairs = list(zip(found_docs, found_metadata))
                doc_meta_pairs.sort(key=lambda x: x[1].get('priority', 0), reverse=True)
                found_docs, found_metadata = zip(*doc_meta_pairs) if doc_meta_pairs else ([], [])
                found_docs, found_metadata = list(found_docs), list(found_metadata)
            
            # Limit to requested number
            return found_docs[:n_results], found_metadata[:n_results]
            
        except Exception as e:
            print(f"[RAG] Error in general search: {e}")
            return [], []
    
    def get_collection_stats(self):
        """Get statistics about the collection"""
        try:
            count = self.collection.count()
            print(f"[RAG] Collection contains {count} chunks")
            return {
                "total_chunks": count,
                "db_path": self.db_path,
                "cache_file": self.cache_file
            }
        except Exception as e:
            print(f"[RAG] Error getting collection stats: {e}")
            return {}
    
    def force_incremental_update(self):
        """Force an incremental update check and processing"""
        print("[RAG] Forcing incremental update check...")
        try:
            if os.path.exists(self.cache_file):
                with open(self.cache_file, 'r') as f:
                    cache_data = json.load(f)
                
                if 'indexed_files' in cache_data:
                    self._should_incremental_update(cache_data)
                else:
                    print("[RAG] No cached file information, cannot perform incremental update")
            else:
                print("[RAG] No cache file found, cannot perform incremental update")
        except Exception as e:
            print(f"[RAG] Error during forced incremental update: {e}")
    
    def rebuild_index(self):
        """Force a complete rebuild of the index"""
        print("[RAG] Forcing complete index rebuild...")
        try:
            self._cleanup_existing_collection()
            try:
                self.collection = self.client.create_collection(
                    name="documents",
                    metadata={"hnsw:space": "cosine", "hnsw:construction_ef": 200, "hnsw:M": 16}
                )
            except Exception as e:
                print(f"[RAG] Failed to create collection with advanced metadata: {e}")
                print(f"[RAG] Creating collection with basic settings...")
                self.collection = self.client.create_collection(name="documents")
            self.load_documents()
            self._save_cache()
            print("[RAG] Index rebuild completed")
        except Exception as e:
            print(f"[RAG] Error during index rebuild: {e}")
    
    def search_api_only(self, query, n_results=5):
        """Search only in API files"""
        return self.search_documents(query, n_results, filter_metadata={"category": "API"}, prioritize_api=False)
    
    def search_exclude_examples(self, query, n_results=5):
        """Search excluding example files"""
        return self.search_documents(query, n_results, filter_metadata={"category": {"$ne": "Example"}}, prioritize_api=False)
    
    def search_high_priority_only(self, query, n_results=5):
        """Search only high priority files (API + Documentation)"""
        return self._search_general(
            self.embeddings.embed_query(query), 
            n_results,
            filter_metadata={"priority": {"$gte": 7}}
        )
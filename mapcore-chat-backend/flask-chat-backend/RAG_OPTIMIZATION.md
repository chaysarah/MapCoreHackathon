# RAG Optimization for Large Datasets (~1GB)

## Key Improvements Made

### 1. Incremental Updates (NEW!)
- **Smart file tracking**: Tracks individual file modifications, not just folder hash
- **Automatic incremental updates**: Only processes new/modified/deleted files
- **Threshold-based rebuilds**: Full rebuild only if >20% of files changed
- **Fast startup**: No rebuilds when no files changed

### 2. ChromaDB Optimization
- **DuckDB + Parquet backend**: Better performance for large datasets
- **HNSW parameters tuned**: `construction_ef=200`, `M=16` for better recall
- **Embedding cache**: 10,000 embeddings cached for faster queries
- **LRU cache policy**: Efficient memory usage

### 2. Parallel Processing
- **Parallel file loading**: Uses ThreadPoolExecutor with 8 workers
- **Batch embedding processing**: 100 chunks per batch to manage memory
- **Memory cleanup**: Garbage collection after each batch

### 3. Optimized Text Splitting
- **Smaller chunks**: 1000 chars (vs 1500) for better granularity
- **Reduced overlap**: 200 chars (vs 300) for less redundancy
- **Code-aware separators**: Splits on functions, classes, comments

### 4. Document Format Support
- **PDF files**: Using PyPDFLoader for PDF document processing
- **Word documents**: DOCX (Docx2txtLoader) and legacy DOC (UnstructuredWordDocumentLoader)
- **Text files**: Multiple encoding support (UTF-8, Latin1, CP1252)
- **Code files**: All major programming languages and config files

### 5. Search Enhancements
- **Metadata filtering**: Filter by language, file type, etc.
- **Return metadata**: Get file info with search results
- **Performance monitoring**: Track search times and stats

## Installation

Install the required dependencies:
```bash
pip install -r requirements.txt
```

**Note**: For Windows users, `python-magic-bin` is included for better file type detection.

## Usage

### 1. Initial Index Building (One-time)
```bash
# Build the index once for your large dataset
python scripts/build_rag_index.py "C:\Path\To\Your\Data"
```

### 2. Using in Your App
```python
from app.services.rag_service import RAGService

# Fast loading (uses existing index with incremental updates)
rag = RAGService(r"C:\Path\To\Your\Data")

# Search with optional filtering
results, metadata = rag.search_documents(
    "function implementation", 
    n_results=5,
    filter_metadata={"language": "C#"}  # Optional filter
)

# Search specifically in documents
doc_results, doc_metadata = rag.search_documents(
    "API documentation",
    n_results=10,
    filter_metadata={"file_type": ".pdf"}  # Only PDF files
)

# Search in Word documents
word_results, word_metadata = rag.search_documents(
    "user manual",
    filter_metadata={"language": "Word Document"}  # DOCX and DOC files
)

# Force incremental update check
rag.force_incremental_update()

# Force complete rebuild if needed
rag.rebuild_index()
```

### 3. Performance Monitoring
```bash
# Monitor performance and get metrics
python scripts/monitor_rag.py "C:\Path\To\Your\Data"
```

## Supported File Types

### Programming Languages
- **C/C++**: `.c`, `.h`, `.cpp`, `.hpp`, `.cc`, `.cxx`
- **C#**: `.cs`, `.csx`, `.cshtml`, `.razor`
- **JavaScript/TypeScript**: `.js`, `.jsx`, `.ts`, `.tsx`
- **Web**: `.html`, `.htm`, `.css`, `.scss`, `.sass`
- **Data**: `.json`, `.xml`, `.xaml`, `.yaml`, `.yml`

### Documentation
- **Markdown**: `.md`, `.markdown`
- **Text**: `.txt`, `.rst`
- **PDF**: `.pdf` (extracted text content)
- **Word**: `.docx` (full support), `.doc` (requires LibreOffice)

**Note**: Legacy `.doc` files require LibreOffice installation. See `DOC_FILE_SUPPORT.md` for alternatives.

### Configuration & Project Files
- **Config**: `.config`, `.settings`, `.ini`, `.conf`
- **Project**: `.csproj`, `.sln`, `.vcxproj`, `.props`, `.targets`
- **Package**: `package.json`, `package-lock.json`, `.npmrc`

## Performance Tuning

### For Different Data Sizes

**Small datasets (< 100MB):**
- chunk_size: 1500
- batch_size: 50
- max_workers: 4

**Medium datasets (100MB - 500MB):**
- chunk_size: 1000
- batch_size: 100
- max_workers: 6

**Large datasets (500MB - 2GB):**
- chunk_size: 800
- batch_size: 100
- max_workers: 8

**Extra large datasets (> 2GB):**
- chunk_size: 600
- batch_size: 150
- max_workers: 12

### Memory Optimization
- Batch processing prevents memory overflow
- Garbage collection after each batch
- Persistent ChromaDB reduces RAM usage
- Parallel loading with thread limits

### Search Performance
- HNSW indexing for fast similarity search
- Metadata filtering reduces search space
- Embedding caching for repeated queries
- Optimized query parameters

## Expected Performance

**Initial Index Building:**
- ~1GB data: 30-60 minutes (one-time)
- Progress tracking with batch updates
- Persistent storage for fast subsequent loads

**Incremental Updates:**
- Adding 1 file: 10-30 seconds (no rebuild!)
- Modifying few files: 1-5 minutes  
- Deleting files: < 10 seconds
- Only rebuilds if >20% of files changed

**Search Performance:**
- Query time: 50-200ms for 5 results
- Metadata filtering: 20-100ms additional
- Cold start: 2-5 seconds to load index
- Warm queries: < 100ms

## Troubleshooting

### Memory Issues
- Reduce `batch_size` if out of memory
- Lower `max_workers` for file loading
- Increase `chunk_size` to reduce total chunks

### Slow Indexing
- Increase `max_workers` (up to CPU cores)
- Use SSD storage for ChromaDB
- Filter out unnecessary file types

### Poor Search Quality
- Increase `hnsw_construction_ef` for better recall
- Adjust `chunk_size` and `chunk_overlap`
- Use metadata filtering for specific searches

### DOC File Issues
- **Error**: `soffice command was not found`
  - **Solution**: Install LibreOffice or convert DOC to DOCX
  - **Alternative**: See `DOC_FILE_SUPPORT.md` for detailed solutions

### Import Errors
- **Missing dependencies**: Run `pip install -r requirements.txt`
- **ChromaDB issues**: Check ChromaDB version compatibility
- **Document loaders**: Ensure all document processing libraries are installed

## File Structure
```
├── scripts/
│   ├── build_rag_index.py    # One-time index building
│   └── monitor_rag.py        # Performance monitoring
├── config/
│   └── rag_config.py         # Configuration settings
└── src/app/services/
    └── rag_service.py        # Optimized RAG service
```

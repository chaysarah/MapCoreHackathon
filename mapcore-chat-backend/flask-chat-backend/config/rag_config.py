# RAG Configuration for Large Datasets (~1GB)

RAG_CONFIG = {
    # Chunking settings
    "chunk_size": 1000,          # Smaller chunks for better granularity
    "chunk_overlap": 200,        # Reduced overlap for less redundancy
    
    # Batch processing
    "batch_size": 100,           # Process embeddings in batches
    "max_workers": 8,            # Parallel file loading threads
    
    # ChromaDB optimization
    "hnsw_space": "cosine",      # Distance metric
    "hnsw_construction_ef": 200, # Higher = better recall, slower build
    "hnsw_m": 16,                # Higher = better recall, more memory
    
    # Search settings
    "default_n_results": 5,      # Default number of search results
    
    # File filtering
    "supported_extensions": [
        # C/C++ files
        '.c', '.h', '.cpp', '.hpp', '.cc', '.cxx',
        # C# files  
        '.cs', '.csx', '.cshtml', '.razor',
        # JavaScript/TypeScript
        '.js', '.jsx', '.ts', '.tsx', '.json',
        # Web files
        '.html', '.htm', '.css', '.scss', '.sass',
        # Configuration
        '.xml', '.xaml', '.config', '.settings',
        # Documentation
        '.txt', '.md', '.markdown', '.rst',
        # Project files
        '.csproj', '.sln', '.vcxproj', '.props', '.targets',
        # Package files
        'package.json', 'package-lock.json', '.npmrc',
        # Other
        '.yaml', '.yml', '.ini', '.conf',
        # Document formats
        '.pdf', '.doc', '.docx'
    ],
    
    # Memory management
    "gc_after_batch": True,      # Run garbage collection after each batch
    "embedding_cache_size": 10000, # ChromaDB embedding cache size
}

# Performance recommendations for different data sizes:
PERFORMANCE_PROFILES = {
    "small": {  # < 100MB
        "chunk_size": 1500,
        "batch_size": 50,
        "max_workers": 4,
        "hnsw_construction_ef": 100,
    },
    "medium": {  # 100MB - 500MB
        "chunk_size": 1000,
        "batch_size": 100,
        "max_workers": 6,
        "hnsw_construction_ef": 150,
    },
    "large": {  # 500MB - 2GB
        "chunk_size": 800,
        "batch_size": 100,
        "max_workers": 8,
        "hnsw_construction_ef": 200,
    },
    "xlarge": {  # > 2GB
        "chunk_size": 600,
        "batch_size": 150,
        "max_workers": 12,
        "hnsw_construction_ef": 300,
    }
}

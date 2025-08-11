#!/usr/bin/env python3
"""
Script to build RAG index for large datasets.
This should be run once to pre-build the index, then your app can load it quickly.
"""

import sys
import os
import time
from pathlib import Path

# Add the src directory to Python path
sys.path.append(str(Path(__file__).parent.parent / "src"))

from app.services.rag_service import RAGService

def main():
    if len(sys.argv) != 2:
        print("Usage: python build_rag_index.py <data_folder_path>")
        print("Example: python build_rag_index.py C:\\Data\\MyCodebase")
        sys.exit(1)
    
    data_folder = sys.argv[1]
    
    if not os.path.exists(data_folder):
        print(f"Error: Folder does not exist: {data_folder}")
        sys.exit(1)
    
    print(f"Building RAG index for: {data_folder}")
    print("This may take a while for large datasets...")
    
    start_time = time.time()
    
    try:
        # Initialize RAG service (this will trigger indexing)
        rag = RAGService(data_folder)
        
        # Get final stats
        stats = rag.get_collection_stats()
        
        end_time = time.time()
        elapsed = end_time - start_time
        
        print(f"\n=== Indexing Complete ===")
        print(f"Total chunks: {stats.get('total_chunks', 0)}")
        print(f"Time taken: {elapsed:.2f} seconds")
        print(f"DB path: {stats.get('db_path', 'Unknown')}")
        print(f"Cache file: {stats.get('cache_file', 'Unknown')}")
        
        # Test search
        print(f"\n=== Testing Search ===")
        results, metadata = rag.search_documents("function", n_results=3)
        print(f"Test search returned {len(results)} results")
        
    except Exception as e:
        print(f"Error during indexing: {e}")
        import traceback
        traceback.print_exc()
        sys.exit(1)

if __name__ == "__main__":
    main()

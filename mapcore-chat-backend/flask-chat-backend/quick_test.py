#!/usr/bin/env python3
"""
Quick test script to force reload and test priority system
"""

import os
import sys
sys.path.append(os.path.join(os.path.dirname(__file__), 'src'))

def quick_test_priorities():
    """Force reload a small subset to test priorities"""
    
    # Delete cache to force reload
    cache_files = [
        "rag_cache.json",
        "../rag_cache.json"
    ]
    
    for cache_file in cache_files:
        if os.path.exists(cache_file):
            os.remove(cache_file)
            print(f"🗑️ Removed cache file: {cache_file}")
    
    # Import and test
    from src.app.services.rag_service import RAGService
    
    rag_folder = os.getenv("RAG_FOLDER_PATH", r"C:\path\to\your\documents")
    if not os.path.exists(rag_folder):
        print(f"❌ RAG folder not found: {rag_folder}")
        return
    
    print("🔄 Testing priority system (this will reload documents)...")
    rag_service = RAGService(rag_folder)
    
    # Test search
    docs, metadata = rag_service.search_documents("polygon", n_results=3)
    
    if metadata:
        print("✅ Priority metadata found:")
        for meta in metadata:
            print(f"  - {meta.get('file_name', 'Unknown')}: {meta.get('category', 'Unknown')} (Priority: {meta.get('priority', 0)})")
    else:
        print("❌ No metadata found - rebuild required")

if __name__ == "__main__":
    quick_test_priorities()

#!/usr/bin/env python3
"""
Script to rebuild RAG index with new priority weighting metadata
"""

import os
import sys
sys.path.append(os.path.join(os.path.dirname(__file__), 'src'))

from src.app.services.rag_service import RAGService

def rebuild_with_priorities():
    """Rebuild the ChromaDB index with new priority metadata"""
    
    # Get RAG folder path
    rag_folder = os.getenv("RAG_FOLDER_PATH", r"C:\path\to\your\documents")
    if not os.path.exists(rag_folder):
        print(f"❌ RAG folder not found: {rag_folder}")
        print("Please set RAG_FOLDER_PATH environment variable or update the path")
        return False
    
    print("🔄 Rebuilding RAG index with priority weighting...")
    print(f"📁 Source folder: {rag_folder}")
    
    try:
        # Initialize RAG service
        rag_service = RAGService(rag_folder)
        
        if not hasattr(rag_service, 'collection'):
            print("❌ RAG service not properly initialized")
            return False
        
        print("🗑️ Forcing complete index rebuild...")
        
        # Force rebuild to include new metadata
        rag_service.rebuild_index()
        
        print("✅ Rebuild completed successfully!")
        
        # Show statistics
        stats = rag_service.get_collection_stats()
        print(f"📊 Total chunks indexed: {stats.get('total_chunks', 0)}")
        
        # Test the new priority system
        print("\n🧪 Testing priority classification...")
        test_query = "polygon API"
        docs, metadata = rag_service.search_documents(test_query, n_results=3)
        
        if metadata:
            print(f"\n🔍 Sample results for '{test_query}':")
            for i, meta in enumerate(metadata[:3]):
                category = meta.get('category', 'Unknown')
                priority = meta.get('priority', 0)
                file_name = meta.get('file_name', 'Unknown')
                
                category_icon = {
                    'API': '🔷',
                    'Documentation': '📚', 
                    'Core': '⚙️',
                    'Example': '📝'
                }.get(category, '❓')
                
                print(f"  {i+1}. {category_icon} {file_name} (Priority: {priority}, Category: {category})")
        
        return True
        
    except Exception as e:
        print(f"❌ Error during rebuild: {e}")
        return False

if __name__ == "__main__":
    print("🚀 RAG Index Rebuild Script")
    print("This will rebuild your ChromaDB with new priority metadata")
    
    # Confirm before proceeding
    response = input("\n⚠️  This will delete and rebuild your entire index. Continue? (y/N): ")
    if response.lower() != 'y':
        print("❌ Rebuild cancelled")
        sys.exit(0)
    
    success = rebuild_with_priorities()
    
    if success:
        print("\n🎉 Index rebuilt successfully with priority weighting!")
        print("💡 Your RAG system now prioritizes API files over examples")
    else:
        print("\n❌ Rebuild failed. Please check the error messages above.")

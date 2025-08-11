#!/usr/bin/env python3
"""
Test script to demonstrate RAG priority weighting for API vs Example files
"""

import os
import sys
sys.path.append(os.path.join(os.path.dirname(__file__), 'src'))

from src.app.services.rag_service import RAGService

def test_priority_search():
    """Test different search methods to show priority weighting"""
    
    # Initialize RAG service (adjust path to your documents)
    rag_folder = os.getenv("RAG_FOLDER_PATH", r"C:\path\to\your\documents")
    if not os.path.exists(rag_folder):
        print(f"❌ RAG folder not found: {rag_folder}")
        print("Please set RAG_FOLDER_PATH environment variable")
        return
    
    print("🔍 Initializing RAG service...")
    rag_service = RAGService(rag_folder)
    
    if not hasattr(rag_service, 'collection'):
        print("❌ RAG service not properly initialized")
        return
    
    # Test query
    query = "how to create a polygon"
    
    print(f"\n🔍 Testing priority search with query: '{query}'")
    print("=" * 60)
    
    # 1. Regular search with API prioritization (default)
    print("\n1️⃣ PRIORITIZED SEARCH (API files first)")
    docs, metadata = rag_service.search_documents(query, n_results=5, prioritize_api=True)
    print_results("Prioritized", docs, metadata)
    
    # 2. Regular search without prioritization
    print("\n2️⃣ REGULAR SEARCH (no prioritization)")
    docs, metadata = rag_service.search_documents(query, n_results=5, prioritize_api=False)
    print_results("Regular", docs, metadata)
    
    # 3. API files only
    print("\n3️⃣ API FILES ONLY")
    docs, metadata = rag_service.search_api_only(query, n_results=5)
    print_results("API Only", docs, metadata)
    
    # 4. Exclude examples
    print("\n4️⃣ EXCLUDE EXAMPLES")
    docs, metadata = rag_service.search_exclude_examples(query, n_results=5)
    print_results("No Examples", docs, metadata)
    
    # 5. High priority only
    print("\n5️⃣ HIGH PRIORITY ONLY (API + Documentation)")
    docs, metadata = rag_service.search_high_priority_only(query, n_results=5)
    print_results("High Priority", docs, metadata)

def print_results(search_type, docs, metadata):
    """Print search results in a formatted way"""
    print(f"Results for {search_type} search:")
    
    if not docs:
        print("  ❌ No results found")
        return
    
    for i, (doc, meta) in enumerate(zip(docs, metadata)):
        category = meta.get('category', 'Unknown')
        priority = meta.get('priority', 0)
        file_name = meta.get('file_name', 'Unknown')
        
        # Color coding for categories
        category_icon = {
            'API': '🔷',
            'Documentation': '📚', 
            'Core': '⚙️',
            'Example': '📝'
        }.get(category, '❓')
        
        print(f"  {i+1}. {category_icon} {file_name} (Priority: {priority}, Category: {category})")
        print(f"     Preview: {doc[:80]}...")
    
    # Summary
    categories = [meta.get('category', 'Unknown') for meta in metadata]
    priorities = [meta.get('priority', 0) for meta in metadata]
    
    category_counts = {}
    for cat in categories:
        category_counts[cat] = category_counts.get(cat, 0) + 1
    
    avg_priority = sum(priorities) / len(priorities) if priorities else 0
    
    print(f"  📊 Summary: {category_counts}, Avg Priority: {avg_priority:.1f}")

def test_file_classification():
    """Test file classification logic"""
    print("\n🏷️ Testing file classification...")
    
    test_files = [
        "src/api/UserController.cs",
        "src/services/MapService.cs", 
        "examples/basic_example.js",
        "samples/getting_started.py",
        "docs/API_Reference.md",
        "README.md",
        "src/core/Engine.cpp",
        "tutorials/polygon_tutorial.html"
    ]
    
    # We'll create a temporary RAG service just for classification
    class TempRAG:
        def _classify_file_type(self, file_path):
            file_name = os.path.basename(file_path).lower()
            dir_path = os.path.dirname(file_path).lower()
            
            api_indicators = ['api', 'interface', 'service', 'controller', 'endpoint']
            example_indicators = ['example', 'sample', 'demo', 'test', 'tutorial']
            doc_indicators = ['readme', 'doc', 'guide', 'manual', 'specification']
            
            full_path = f"{dir_path}/{file_name}"
            
            if any(indicator in full_path for indicator in api_indicators):
                return 'API'
            elif any(indicator in full_path for indicator in doc_indicators):
                return 'Documentation'
            elif any(indicator in full_path for indicator in example_indicators):
                return 'Example'
            else:
                return 'Core'
        
        def _get_file_priority(self, file_path):
            category = self._classify_file_type(file_path)
            priority_map = {'API': 10, 'Documentation': 7, 'Core': 5, 'Example': 3}
            return priority_map.get(category, 5)
    
    temp_rag = TempRAG()
    
    for file_path in test_files:
        category = temp_rag._classify_file_type(file_path)
        priority = temp_rag._get_file_priority(file_path)
        
        category_icon = {
            'API': '🔷',
            'Documentation': '📚', 
            'Core': '⚙️',
            'Example': '📝'
        }.get(category, '❓')
        
        print(f"  {category_icon} {file_path} → {category} (Priority: {priority})")

if __name__ == "__main__":
    print("🚀 RAG Priority Testing Script")
    print("Testing how API files get prioritized over examples...")
    
    # Test file classification first
    test_file_classification()
    
    # Test actual search if RAG service is available
    try:
        test_priority_search()
    except Exception as e:
        print(f"\n❌ Error during search testing: {e}")
        print("Make sure your RAG service is properly configured and has indexed documents.")
    
    print("\n✅ Testing complete!")
    print("\n💡 Tips:")
    print("- API files should appear first in prioritized search")
    print("- Example files should have lower priority")
    print("- You can adjust priorities in config/rag_priorities.py")

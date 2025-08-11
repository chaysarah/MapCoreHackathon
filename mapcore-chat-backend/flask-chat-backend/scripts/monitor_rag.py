#!/usr/bin/env python3
"""
RAG Performance Monitor
Monitor search performance and collection statistics
"""

import sys
import os
import time
import json
from pathlib import Path

# Add the src directory to Python path
sys.path.append(str(Path(__file__).parent.parent / "src"))

from app.services.rag_service import RAGService

def benchmark_search(rag_service, queries, n_results=5):
    """Benchmark search performance with multiple queries"""
    print(f"\n=== Search Benchmark ===")
    
    total_time = 0
    successful_searches = 0
    
    for i, query in enumerate(queries, 1):
        print(f"\nQuery {i}: '{query}'")
        
        start_time = time.time()
        try:
            results, metadata = rag_service.search_documents(query, n_results=n_results)
            end_time = time.time()
            
            search_time = end_time - start_time
            total_time += search_time
            successful_searches += 1
            
            print(f"  Results: {len(results)}")
            print(f"  Time: {search_time:.3f}s")
            
            if metadata:
                languages = set(meta.get('language', 'Unknown') for meta in metadata)
                print(f"  Languages: {', '.join(languages)}")
                
        except Exception as e:
            print(f"  Error: {e}")
    
    if successful_searches > 0:
        avg_time = total_time / successful_searches
        print(f"\n=== Summary ===")
        print(f"Total queries: {len(queries)}")
        print(f"Successful: {successful_searches}")
        print(f"Average search time: {avg_time:.3f}s")
        print(f"Total time: {total_time:.3f}s")
    
    return total_time, successful_searches

def main():
    if len(sys.argv) != 2:
        print("Usage: python monitor_rag.py <data_folder_path>")
        sys.exit(1)
    
    data_folder = sys.argv[1]
    
    if not os.path.exists(data_folder):
        print(f"Error: Folder does not exist: {data_folder}")
        sys.exit(1)
    
    print(f"Monitoring RAG performance for: {data_folder}")
    
    try:
        # Load existing RAG service
        print("Loading RAG service...")
        start_time = time.time()
        rag = RAGService(data_folder)
        load_time = time.time() - start_time
        
        print(f"RAG service loaded in {load_time:.3f}s")
        
        # Get collection stats
        stats = rag.get_collection_stats()
        print(f"\n=== Collection Statistics ===")
        for key, value in stats.items():
            print(f"{key}: {value}")
        
        # Test queries for different code search scenarios
        test_queries = [
            "function definition",
            "class implementation", 
            "error handling",
            "main entry point",
            "configuration settings",
            "import statements",
            "variable declaration",
            "loop iteration",
            "conditional logic",
            "memory allocation"
        ]
        
        # Benchmark search performance
        total_time, successful = benchmark_search(rag, test_queries)
        
        # Save performance metrics
        metrics = {
            "timestamp": time.time(),
            "load_time": load_time,
            "collection_stats": stats,
            "search_benchmark": {
                "total_queries": len(test_queries),
                "successful_queries": successful,
                "total_search_time": total_time,
                "average_search_time": total_time / successful if successful > 0 else 0
            }
        }
        
        # Save to JSON file
        metrics_file = os.path.join(os.path.dirname(data_folder), "rag_performance_metrics.json")
        with open(metrics_file, 'w') as f:
            json.dump(metrics, f, indent=2)
        
        print(f"\nPerformance metrics saved to: {metrics_file}")
        
    except Exception as e:
        print(f"Error during monitoring: {e}")
        import traceback
        traceback.print_exc()
        sys.exit(1)

if __name__ == "__main__":
    main()

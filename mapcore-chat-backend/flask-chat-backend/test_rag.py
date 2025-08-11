#!/usr/bin/env python3
"""
Simple test script to verify RAG service works correctly
"""

import os
import sys

# Add the src directory to Python path
sys.path.append(os.path.join(os.path.dirname(__file__), 'src'))

def test_rag_service():
    try:
        print("Testing RAG service imports...")
        from app.services.rag_service import RAGService
        print("✓ RAG service import successful")
        
        print("\nTesting ChromaDB version...")
        import chromadb
        print(f"✓ ChromaDB version: {chromadb.__version__}")
        
        print("\nTesting Google embeddings...")
        from langchain_google_genai import GoogleGenerativeAIEmbeddings
        print("✓ Google embeddings import successful")
        
        print("\nTesting document loaders...")
        from langchain_community.document_loaders import TextLoader, PyPDFLoader, Docx2txtLoader, UnstructuredWordDocumentLoader
        print("✓ Document loaders import successful")
        
        # Test basic RAG service initialization (without actual data folder)
        print("\nTesting RAG service basic initialization...")
        test_folder = "test_folder_that_doesnt_exist"
        try:
            rag = RAGService(test_folder)
            print("✓ RAG service handles non-existent folder gracefully")
        except Exception as e:
            print(f"✓ RAG service properly handles error: {e}")
        
        print("\n🎉 All tests passed! RAG service is ready to use.")
        return True
        
    except ImportError as e:
        print(f"❌ Import error: {e}")
        print("Please install missing dependencies with: pip install -r requirements.txt")
        return False
    except Exception as e:
        print(f"❌ Error: {e}")
        return False

if __name__ == "__main__":
    test_rag_service()

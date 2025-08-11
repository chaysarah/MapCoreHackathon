#!/usr/bin/env python3
"""
Installation script for RAG dependencies
Run this if pip install -r requirements.txt fails
"""

import subprocess
import sys

def install_package(package):
    """Install a package using pip"""
    try:
        subprocess.check_call([sys.executable, "-m", "pip", "install", package])
        print(f"✓ Successfully installed: {package}")
        return True
    except subprocess.CalledProcessError:
        print(f"✗ Failed to install: {package}")
        return False

def main():
    """Install all required packages"""
    print("Installing RAG dependencies...")
    
    # Core packages
    core_packages = [
        "flask>=2.0,<3.0",
        "werkzeug<2.3.0", 
        "Flask-Cors==3.0.10",
        "python-dotenv>=0.21.0"
    ]
    
    # RAG packages
    rag_packages = [
        "langchain>=0.1.0",
        "langchain-community>=0.0.10", 
        "langchain-google-genai>=0.0.5",
        "chromadb>=0.4.0",
        "google-generativeai>=0.3.1"
    ]
    
    # Document processing packages
    doc_packages = [
        "pypdf>=3.0.0",
        "python-docx>=0.8.11",
        "docx2txt>=0.8",
        "unstructured>=0.10.0"
    ]
    
    # Optional packages
    optional_packages = [
        "python-magic-bin>=0.4.14"  # For Windows
    ]
    
    all_packages = core_packages + rag_packages + doc_packages + optional_packages
    
    failed_packages = []
    for package in all_packages:
        if not install_package(package):
            failed_packages.append(package)
    
    print(f"\n=== Installation Summary ===")
    print(f"Successfully installed: {len(all_packages) - len(failed_packages)}/{len(all_packages)} packages")
    
    if failed_packages:
        print(f"Failed packages: {failed_packages}")
        print("You can try installing these manually or continue without them.")
    else:
        print("🎉 All packages installed successfully!")
        print("Your RAG system is ready to use!")

if __name__ == "__main__":
    main()

import os
import google.generativeai as genai
from dotenv import load_dotenv
from .rag_service import RAGService

load_dotenv()

class ChatService:
    def __init__(self):
        api_key = os.getenv("GOOGLE_API_KEY")
        genai.configure(api_key=api_key)
        
        self.model_name = os.getenv("GOOGLE_MODEL", "gemini-1.5-flash")
        self.model = genai.GenerativeModel(self.model_name)
        
        # Initialize RAG with your folder path
        rag_folder = os.getenv("RAG_FOLDER_PATH", r"C:\path\to\your\documents")
        self.rag_service = RAGService(rag_folder) if os.path.exists(rag_folder) else None

    def process_message(self, message):
        if not message:
            return "Please provide a message."

        try:
            # Get relevant context from RAG
            context = ""
            if self.rag_service:
                relevant_docs = self.rag_service.search_documents(message)
                context = "\n".join(relevant_docs)
            
            # Create prompt with context
            if context:
                prompt = f"""Based on the following context, answer the user's question:

Context:
{context}

Question: {message}

Answer:"""
            else:
                prompt = message
            
            response = self.model.generate_content(prompt)
            return response.text
            
        except Exception as e:
            return f"Error processing message: {e}"

    # you can remove or implement these helpers as needed
    def _analyze_intent(self, message):
        pass

    def _generate_response(self, intent, message):
        pass
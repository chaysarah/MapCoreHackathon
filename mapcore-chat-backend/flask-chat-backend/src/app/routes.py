from flask import Blueprint, request, jsonify
from .services.chat_service import ChatService

chat_bp = Blueprint('chat', __name__)
chat_service = ChatService()

@chat_bp.route('/chat', methods=['POST'])
def chat():
    data = request.get_json()
    message = data.get('message')
    
    if not message:
        return jsonify({'error': 'Message is required'}), 400
    
    response = chat_service.process_message(message)
    return jsonify({'response': response}), 200

@chat_bp.route('/debug-rag', methods=['POST'])
def debug_rag():
    """Debug endpoint to test RAG directly"""
    data = request.get_json()
    query = data.get('query', 'test')
    
    if not chat_service.rag_service:
        return jsonify({'error': 'RAG service not initialized'}), 400
    
    results = chat_service.rag_service.search_documents(query)
    return jsonify({
        'query': query,
        'results': results,
        'count': len(results)
    }), 200
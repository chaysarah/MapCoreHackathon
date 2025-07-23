from flask import Blueprint, request, jsonify
import os
from .services.chat_service import ChatService

chat_bp = Blueprint('chat', __name__)
chat_service = ChatService()

@chat_bp.route('/chat', methods=['POST'])
def chat():
    data = request.get_json()
    message = data.get('message')
    user_id = data.get('user_id')  # Optional user ID for memory context
    
    if not message:
        return jsonify({'error': 'Message is required'}), 400
    
    response = chat_service.process_message(message, user_id)
    return jsonify({
        'response': response,
        'user_id': user_id or 'anonymous'
    }), 200

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

# User Memory Routes
@chat_bp.route('/user-memory/<user_id>', methods=['GET'])
def get_user_memory(user_id):
    """Get user's conversation history and context"""
    try:
        summary = chat_service.get_user_memory_summary(user_id)
        return jsonify(summary), 200
    except Exception as e:
        return jsonify({'error': f'Could not retrieve user memory: {str(e)}'}), 400

@chat_bp.route('/user-memory/<user_id>/conversations', methods=['GET'])
def get_user_conversations(user_id):
    """Get detailed conversation history for a user"""
    try:
        limit = int(request.args.get('limit', 10))
        user_memory = chat_service._load_user_memory(user_id)
        
        conversations = user_memory['conversations'][-limit:] if user_memory['conversations'] else []
        
        return jsonify({
            'user_id': user_id,
            'conversations': conversations,
            'total_count': len(user_memory['conversations']),
            'returned_count': len(conversations)
        }), 200
    except Exception as e:
        return jsonify({'error': f'Could not retrieve conversations: {str(e)}'}), 400

@chat_bp.route('/user-memory/<user_id>', methods=['DELETE'])
def clear_user_memory(user_id):
    """Clear user's conversation history"""
    try:
        success = chat_service.clear_user_memory(user_id)
        if success:
            return jsonify({'message': f'User memory cleared for {user_id}'}), 200
        else:
            return jsonify({'error': 'User memory not found or could not be cleared'}), 404
    except Exception as e:
        return jsonify({'error': f'Could not clear user memory: {str(e)}'}), 400

@chat_bp.route('/user-memory/<user_id>/topics', methods=['GET'])
def get_user_topics(user_id):
    """Get user's tracked conversation topics"""
    try:
        user_memory = chat_service._load_user_memory(user_id)
        
        return jsonify({
            'user_id': user_id,
            'topics': user_memory.get('context_topics', []),
            'preferences': user_memory.get('preferences', {}),
            'last_interaction': user_memory['conversations'][-1]['timestamp'] if user_memory['conversations'] else None
        }), 200
    except Exception as e:
        return jsonify({'error': f'Could not retrieve user topics: {str(e)}'}), 400

@chat_bp.route('/user-memory', methods=['GET'])
def list_all_users():
    """List all users with conversation history"""
    try:
        memory_dir = chat_service.memory_dir
        
        if not os.path.exists(memory_dir):
            return jsonify({'users': [], 'count': 0}), 200
        
        users = []
        for filename in os.listdir(memory_dir):
            if filename.endswith('.json'):
                user_id = filename[:-5]  # Remove .json extension
                summary = chat_service.get_user_memory_summary(user_id)
                users.append(summary)
        
        return jsonify({
            'users': users,
            'count': len(users)
        }), 200
    except Exception as e:
        return jsonify({'error': f'Could not list users: {str(e)}'}), 400

@chat_bp.route('/user-memory/<user_id>/export', methods=['GET'])
def export_user_memory(user_id):
    """Export user's complete conversation history"""
    try:
        user_memory = chat_service._load_user_memory(user_id)
        
        # Format for export
        export_data = {
            'user_id': user_id,
            'export_timestamp': chat_service._generate_timestamp(),
            'conversation_count': len(user_memory['conversations']),
            'memory_data': user_memory
        }
        
        return jsonify(export_data), 200
    except Exception as e:
        return jsonify({'error': f'Could not export user memory: {str(e)}'}), 400

# Developer Report Routes (existing)
@chat_bp.route('/developer-reports', methods=['GET'])
def get_developer_reports():
    """Endpoint for developers to view bug reports and feature requests"""
    report_type = request.args.get('type')  # 'bug', 'missing-feature', or None for all
    
    if not chat_service.rag_service:
        return jsonify({'error': 'Service not initialized'}), 400
    
    reports = chat_service.get_developer_reports(report_type)
    return jsonify({
        'reports': reports,
        'count': len(reports),
        'type_filter': report_type
    }), 200

@chat_bp.route('/developer-reports/summary', methods=['GET'])
def get_reports_summary():
    """Get quick summary of recent reports"""
    try:
        summary_file = os.path.join(chat_service.reports_dir, "summary.txt")
        if os.path.exists(summary_file):
            with open(summary_file, 'r', encoding='utf-8') as f:
                lines = f.readlines()[-20:]  # Last 20 entries
            return jsonify({'summary': lines}), 200
        else:
            return jsonify({'summary': []}), 200
    except Exception as e:
        return jsonify({'error': str(e)}), 500

# System Health and Status Routes
@chat_bp.route('/system/status', methods=['GET'])
def get_system_status():
    """Get overall system health status"""
    try:
        status = {
            'rag_service': chat_service.rag_service is not None,
            'rag_documents': chat_service.rag_service.collection.count() if chat_service.rag_service else 0,
            'email_configured': all([chat_service.email_user, chat_service.email_password, chat_service.developer_email]),
            'memory_dir_exists': os.path.exists(chat_service.memory_dir),
            'reports_dir_exists': os.path.exists(chat_service.reports_dir),
            'active_users': len([f for f in os.listdir(chat_service.memory_dir) if f.endswith('.json')]) if os.path.exists(chat_service.memory_dir) else 0,
            'total_reports': len([f for f in os.listdir(chat_service.reports_dir) if f.endswith('.json')]) if os.path.exists(chat_service.reports_dir) else 0
        }
        
        return jsonify(status), 200
    except Exception as e:
        return jsonify({'error': f'Could not get system status: {str(e)}'}), 500

@chat_bp.route('/system/test-email', methods=['POST'])
def test_email_config():
    """Test email configuration"""
    try:
        success = chat_service.test_email_config()
        return jsonify({
            'email_test_passed': success,
            'configured': all([chat_service.email_user, chat_service.email_password, chat_service.developer_email])
        }), 200
    except Exception as e:
        return jsonify({'error': f'Email test failed: {str(e)}'}), 500

# Analytics Routes
@chat_bp.route('/analytics/user-activity', methods=['GET'])
def get_user_activity():
    """Get user activity analytics"""
    try:
        days = int(request.args.get('days', 7))  # Last 7 days by default
        
        analytics = chat_service.get_user_activity_analytics(days)
        return jsonify(analytics), 200
    except Exception as e:
        return jsonify({'error': f'Could not get analytics: {str(e)}'}), 500

@chat_bp.route('/analytics/intent-distribution', methods=['GET'])
def get_intent_distribution():
    """Get distribution of message intents (bugs, features, questions)"""
    try:
        days = int(request.args.get('days', 30))  # Last 30 days by default
        
        distribution = chat_service.get_intent_distribution(days)
        return jsonify(distribution), 200
    except Exception as e:
        return jsonify({'error': f'Could not get intent distribution: {str(e)}'}), 500
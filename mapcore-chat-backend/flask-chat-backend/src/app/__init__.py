from flask import Flask
from flask_cors import CORS
from .routes import chat_bp

def create_app():
    app = Flask(__name__)
    CORS(app)  # Enable CORS for all routes
    app.register_blueprint(chat_bp, url_prefix='/api')
    return app
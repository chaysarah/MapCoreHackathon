import os
import json
import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart
from datetime import datetime, timedelta
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
        
        # Create reports directory
        self.reports_dir = os.path.join(os.path.dirname(rag_folder), "developer_reports")
        if not os.path.exists(self.reports_dir):
            os.makedirs(self.reports_dir)
        
        # Create user memory directory
        self.memory_dir = os.path.join(os.path.dirname(rag_folder), "user_memory")
        if not os.path.exists(self.memory_dir):
            os.makedirs(self.memory_dir)
        
        # Email configuration
        self.smtp_server = os.getenv("SMTP_SERVER", "smtp.gmail.com")
        self.smtp_port = int(os.getenv("SMTP_PORT", "587"))
        self.email_user = os.getenv("EMAIL_USER")
        self.email_password = os.getenv("EMAIL_PASSWORD")
        self.developer_email = os.getenv("DEVELOPER_EMAIL", "tova.barzel@mapcore.com")
        
        # Memory settings
        self.max_memory_entries = int(os.getenv("MAX_MEMORY_ENTRIES", "20"))
        self.memory_retention_days = int(os.getenv("MEMORY_RETENTION_DAYS", "30"))

    def process_message(self, message, user_id=None):
        if not message:
            return "Please provide a message."

        try:
            # Get or create user ID
            if not user_id:
                user_id = self._generate_anonymous_user_id()
            
            # Load user memory
            user_memory = self._load_user_memory(user_id)
            
            # Add current message to memory
            self._add_to_memory(user_memory, message, "user")
            
            # First, classify the message intent
            intent = self._analyze_intent(message, user_memory)
            print(f"[CHAT] Detected intent: {intent} for user: {user_id}")
            
            # Report to developers if needed
            if intent in ['bug', 'missing-feature']:
                report_id = self._report_to_developers(message, intent, user_memory)
                
                # Send email for bugs
                if intent == 'bug':
                    self._send_bug_email(message, report_id, user_memory)
            
            # Get relevant context from RAG
            context = ""
            if self.rag_service:
                # Use both current message and recent conversation for better context
                search_query = self._build_search_query(message, user_memory)
                relevant_docs = self.rag_service.search_documents(search_query)
                context = "\n".join(relevant_docs)
            
            # Create enhanced prompt based on intent and memory
            response = self._generate_response(intent, message, context, user_memory)
            
            # Add response to memory
            self._add_to_memory(user_memory, response, "assistant")
            
            # Save updated memory
            self._save_user_memory(user_id, user_memory)
            
            return response
            
        except Exception as e:
            return f"Error processing message: {e}"

    def _generate_anonymous_user_id(self):
        """Generate a simple anonymous user ID based on timestamp"""
        return f"anon_{datetime.now().strftime('%Y%m%d_%H%M%S')}"
    
    def _load_user_memory(self, user_id):
        """Load user's conversation memory"""
        try:
            memory_file = os.path.join(self.memory_dir, f"{user_id}.json")
            
            if os.path.exists(memory_file):
                with open(memory_file, 'r', encoding='utf-8') as f:
                    memory_data = json.load(f)
                
                # Clean old entries
                cutoff_date = datetime.now() - timedelta(days=self.memory_retention_days)
                memory_data['conversations'] = [
                    conv for conv in memory_data['conversations']
                    if datetime.fromisoformat(conv['timestamp']) > cutoff_date
                ]
                
                print(f"[MEMORY] Loaded {len(memory_data['conversations'])} conversation entries for user {user_id}")
                return memory_data
            else:
                print(f"[MEMORY] Creating new memory for user {user_id}")
                return {
                    "user_id": user_id,
                    "created_at": datetime.now().isoformat(),
                    "conversations": [],
                    "preferences": {},
                    "context_topics": []
                }
                
        except Exception as e:
            print(f"[MEMORY] Error loading user memory: {e}")
            return {
                "user_id": user_id,
                "created_at": datetime.now().isoformat(),
                "conversations": [],
                "preferences": {},
                "context_topics": []
            }

    def _add_to_memory(self, user_memory, message, role):
        """Add a message to user memory"""
        try:
            entry = {
                "timestamp": datetime.now().isoformat(),
                "role": role,  # "user" or "assistant"
                "message": message,
                "message_length": len(message)
            }
            
            user_memory['conversations'].append(entry)
            
            # Keep only recent conversations
            if len(user_memory['conversations']) > self.max_memory_entries:
                user_memory['conversations'] = user_memory['conversations'][-self.max_memory_entries:]
            
            # Update context topics for user messages
            if role == "user":
                self._update_context_topics(user_memory, message)
                
        except Exception as e:
            print(f"[MEMORY] Error adding to memory: {e}")

    def _update_context_topics(self, user_memory, message):
        """Extract and update context topics from user messages"""
        try:
            # Simple keyword extraction for topics
            technical_keywords = [
                'mapcore', 'device', 'layer', 'api', 'c++', 'c#', 'react', 
                'javascript', 'html', 'css', 'component', 'function', 'class',
                'interface', 'struct', 'pointer', 'array', 'string', 'json',
                'xml', 'database', 'file', 'memory', 'performance', 'bug',
                'error', 'crash', 'feature', 'enhancement'
            ]
            
            message_lower = message.lower()
            found_topics = [keyword for keyword in technical_keywords if keyword in message_lower]
            
            # Update context topics
            for topic in found_topics:
                if topic not in user_memory['context_topics']:
                    user_memory['context_topics'].append(topic)
            
            # Keep only recent topics
            if len(user_memory['context_topics']) > 10:
                user_memory['context_topics'] = user_memory['context_topics'][-10:]
                
        except Exception as e:
            print(f"[MEMORY] Error updating context topics: {e}")

    def _build_search_query(self, current_message, user_memory):
        """Build enhanced search query using current message and conversation context"""
        try:
            # Get recent user messages for context
            recent_messages = [
                conv['message'] for conv in user_memory['conversations'][-5:]
                if conv['role'] == 'user'
            ]
            
            # Combine current message with recent context
            if recent_messages:
                search_query = f"{current_message} {' '.join(recent_messages[-2:])}"
            else:
                search_query = current_message
            
            # Add relevant topics
            if user_memory['context_topics']:
                search_query += f" {' '.join(user_memory['context_topics'][-3:])}"
            
            return search_query[:500]  # Limit query length
            
        except Exception as e:
            print(f"[MEMORY] Error building search query: {e}")
            return current_message

    def _save_user_memory(self, user_id, user_memory):
        """Save user memory to file"""
        try:
            memory_file = os.path.join(self.memory_dir, f"{user_id}.json")
            user_memory['last_updated'] = datetime.now().isoformat()
            
            with open(memory_file, 'w', encoding='utf-8') as f:
                json.dump(user_memory, f, indent=2, ensure_ascii=False)
                
            print(f"[MEMORY] Saved memory for user {user_id}")
            
        except Exception as e:
            print(f"[MEMORY] Error saving user memory: {e}")

    def _analyze_intent(self, message, user_memory):
        """Use AI to classify intent with conversation context"""
        try:
            # Build context from recent conversation
            conversation_context = ""
            if user_memory['conversations']:
                recent_conv = user_memory['conversations'][-4:]  # Last 4 exchanges
                conversation_context = "\n".join([
                    f"{conv['role']}: {conv['message']}" 
                    for conv in recent_conv
                ])
            
            classification_prompt = f"""
Analyze the following user message and classify it into one of these categories:
- "bug": The user is reporting an error, crash, malfunction, or something not working as expected
- "missing-feature": The user is requesting a new feature, enhancement, or functionality that doesn't exist
- "question": A regular question about how to use existing functionality

Previous conversation context:
{conversation_context}

Current user message: "{message}"

Examples:
- "The app crashes when I click submit" → bug
- "Can you add dark mode?" → missing-feature  
- "How do I create a new project?" → question

Consider the conversation context when classifying. If the user is following up on a previous topic, maintain consistency.

Respond with only one word: bug, missing-feature, or question
"""
            
            response = self.model.generate_content(classification_prompt)
            intent = response.text.strip().lower()
            
            # Validate response
            if intent not in ['bug', 'missing-feature', 'question']:
                print(f"[CHAT] Invalid intent detected: {intent}, defaulting to 'question'")
                return 'question'
            
            return intent
            
        except Exception as e:
            print(f"[CHAT] Error analyzing intent: {e}")
            # Fallback intent classification
            return self._fallback_intent_classification(message)

    def _fallback_intent_classification(self, message):
        """Simple keyword-based intent classification when API quota is exceeded"""
        message_lower = message.lower()
        
        # Bug keywords
        bug_keywords = ['crash', 'error', 'bug', 'broken', 'not working', 'fails', 'issue', 'problem', 'exception', 'freeze', 'hang']
        
        # Feature request keywords
        feature_keywords = ['add', 'need', 'want', 'could you', 'can you', 'feature', 'enhancement', 'improve', 'suggestion']
        
        # Check for bug indicators
        if any(keyword in message_lower for keyword in bug_keywords):
            return 'bug'
        
        # Check for feature request indicators
        if any(keyword in message_lower for keyword in feature_keywords):
            return 'missing-feature'
        
        return 'question'

    def _generate_response(self, intent, message, context, user_memory):
        """Generate appropriate response based on intent and user memory"""
        try:
            if intent == 'bug':
                base_response = "I understand you've encountered an issue. I've reported this to our development team for investigation and sent an email notification to our developers."
                
            elif intent == 'missing-feature':
                base_response = "Thank you for the feature suggestion! I've forwarded your request to our development team for consideration."
                
            else:  # regular question
                base_response = ""
            
            # Build conversation context
            conversation_context = ""
            if user_memory['conversations']:
                recent_conv = user_memory['conversations'][-6:]  # Last 6 exchanges
                conversation_context = "\n".join([
                    f"{conv['role']}: {conv['message'][:200]}..." if len(conv['message']) > 200 
                    else f"{conv['role']}: {conv['message']}"
                    for conv in recent_conv
                ])
            
            # Create context-aware prompt with memory
            if context:
                prompt = f"""You are a helpful AI assistant for MapCore systems. Based on the conversation history and technical documentation, provide a helpful response.

Previous conversation:
{conversation_context}

Technical documentation context:
{context}

Current user's {intent}: {message}

{base_response}

Please provide a helpful response that:
1. Considers the conversation history
2. References relevant documentation when applicable
3. Maintains consistency with previous responses
4. Is personalized to the user's technical level and interests

Response:"""
            else:
                prompt = f"""You are a helpful AI assistant for MapCore systems.

Previous conversation:
{conversation_context}

Current user's {intent}: {message}

{base_response}

Please provide a helpful response that considers the conversation history and maintains consistency.

Response:"""
            
            try:
                response = self.model.generate_content(prompt)
                ai_response = response.text
            except Exception as e:
                if "429" in str(e) and "quota" in str(e).lower():
                    print("[CHAT] API quota exceeded, using fallback response")
                    ai_response = self._generate_fallback_response(intent, message, context, user_memory)
                else:
                    raise e
            
            # Add developer notification to response
            if intent in ['bug', 'missing-feature']:
                existing_reports = [f for f in os.listdir(self.reports_dir) if f.startswith(intent) and f.endswith('.json')]
                report_num = len(existing_reports)
                
                if intent == 'bug':
                    dev_note = f"\n\n📧 Note: This bug has been automatically reported to our development team and an email notification has been sent to tova.barzel@mapcore.com (Report ID: bug_{report_num:04d})."
                else:
                    dev_note = f"\n\n📝 Note: This feature request has been automatically reported to our development team (Report ID: {intent}_{report_num:04d})."
                
                return ai_response + dev_note
            
            return ai_response
            
        except Exception as e:
            return f"Error generating response: {e}"

    def _generate_fallback_response(self, intent, message, context, user_memory):
        """Generate a basic response when API quota is exceeded"""
        
        # Check if user has asked similar questions before
        similar_topics = []
        if user_memory['conversations']:
            for conv in user_memory['conversations'][-10:]:
                if conv['role'] == 'user':
                    # Simple similarity check
                    words_in_common = set(message.lower().split()) & set(conv['message'].lower().split())
                    if len(words_in_common) > 2:
                        similar_topics.append(conv['message'])
        
        if intent == 'bug':
            return "I understand you've encountered an issue. I've reported this to our development team for investigation and sent an email notification to our developers. Due to high demand, I'm currently operating in limited mode, but your report has been logged successfully."
        
        elif intent == 'missing-feature':
            return "Thank you for the feature suggestion! I've forwarded your request to our development team for consideration. Due to high demand, I'm currently operating in limited mode, but your request has been logged successfully."
        
        else:  # question
            if context:
                response = f"Based on the available documentation, here are some relevant code examples that might help with your question:\n\n{context[:1000]}..."
                if similar_topics:
                    response += f"\n\nI notice you've asked about similar topics before. Due to high demand, I'm currently operating in limited mode, but feel free to refer to our previous conversation for additional context."
                return response
            else:
                if similar_topics:
                    return f"I see you're asking about '{message}'. Based on our previous conversations, this seems related to topics we've discussed. Due to high API demand, I'm currently operating in limited mode. Please refer to our previous discussion or contact our support team for detailed assistance."
                else:
                    return f"I apologize, but due to high API demand, I'm currently operating in limited mode. Your question about '{message}' has been noted. Please contact our support team for detailed assistance, or try again later when the service is fully available."

    def get_user_memory_summary(self, user_id):
        """Get a summary of user's conversation history"""
        try:
            user_memory = self._load_user_memory(user_id)
            
            return {
                "user_id": user_id,
                "total_conversations": len(user_memory['conversations']),
                "recent_topics": user_memory['context_topics'],
                "last_interaction": user_memory['conversations'][-1]['timestamp'] if user_memory['conversations'] else None,
                "preferences": user_memory.get('preferences', {}),
                "created_at": user_memory.get('created_at')
            }
            
        except Exception as e:
            return {"error": f"Could not load user memory: {e}"}

    def clear_user_memory(self, user_id):
        """Clear a user's memory (for privacy/testing)"""
        try:
            memory_file = os.path.join(self.memory_dir, f"{user_id}.json")
            if os.path.exists(memory_file):
                os.remove(memory_file)
                return True
            return False
        except Exception as e:
            print(f"Error clearing user memory: {e}")
            return False

    def _send_bug_email(self, user_message, report_id, user_memory):
        """Send email notification for bug reports"""
        try:
            if not all([self.email_user, self.email_password, self.developer_email]):
                print("[EMAIL] Email configuration missing, skipping email notification")
                self._log_email_fallback(user_message, report_id, "Missing configuration")
                return False

            # Create email content
            subject = f"🐛 Bug Report #{report_id} - MapCore Chat System"
            
            # Get the full report for email
            report_file = os.path.join(self.reports_dir, f"bug_{report_id:04d}.json")
            detailed_analysis = ""
            
            if os.path.exists(report_file):
                with open(report_file, 'r', encoding='utf-8') as f:
                    report_data = json.load(f)
                    detailed_analysis = report_data.get('ai_analysis', '')

            body = f"""
Bug Report Notification
=======================

Report ID: bug_{report_id:04d}
Timestamp: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}

User Message:
"{user_message}"

AI Analysis:
{detailed_analysis}

System Information:
- Source: MapCore Chat Backend
- RAG Service: {'Enabled' if self.rag_service else 'Disabled'}
- Model: {self.model_name}

Report File: {report_file}

Please investigate this issue and update the report status accordingly.

---
This is an automated message from the MapCore Chat System.
"""

            # Create email message
            msg = MIMEMultipart()
            msg['From'] = self.email_user
            msg['To'] = self.developer_email
            msg['Subject'] = subject
            
            msg.attach(MIMEText(body, 'plain'))

            # Send email with better error handling
            print(f"[EMAIL] Sending bug report to {self.developer_email}...")
            
            try:
                with smtplib.SMTP(self.smtp_server, self.smtp_port) as server:
                    server.starttls()
                    server.login(self.email_user, self.email_password)
                    text = msg.as_string()
                    server.sendmail(self.email_user, self.developer_email, text)
                
                print(f"[EMAIL] ✓ Bug report email sent successfully!")
                return True
                
            except smtplib.SMTPAuthenticationError as e:
                print(f"[EMAIL] ✗ Authentication failed: {e}")
                print("[EMAIL] Tip: Make sure you're using a Gmail App Password, not your regular password")
                self._log_email_fallback(user_message, report_id, f"Authentication error: {e}")
                return False
                
            except smtplib.SMTPException as e:
                print(f"[EMAIL] ✗ SMTP error: {e}")
                self._log_email_fallback(user_message, report_id, f"SMTP error: {e}")
                return False
                
        except Exception as e:
            print(f"[EMAIL] ✗ Failed to send bug report email: {e}")
            self._log_email_fallback(user_message, report_id, f"General error: {e}")
            return False

    def _log_email_fallback(self, user_message, report_id, error_reason):
        """Log email failures for manual follow-up"""
        try:
            fallback_file = os.path.join(self.reports_dir, "email_failures.txt")
            timestamp = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
            
            with open(fallback_file, 'a', encoding='utf-8') as f:
                f.write(f"""
[{timestamp}] EMAIL FAILURE
Report ID: bug_{report_id:04d}
Developer Email: {self.developer_email}
Error: {error_reason}
User Message: {user_message[:200]}...
---
""")
            
            print(f"[EMAIL] Logged email failure to {fallback_file}")
            
        except Exception as e:
            print(f"[EMAIL] Could not log email failure: {e}")

    def _report_to_developers(self, message, intent, user_memory):
        """Create and save a report for developers - returns report ID"""
        try:
            # Generate detailed analysis
            analysis_prompt = f"""
A user has reported a {intent}. Please analyze this message and provide:
1. Summary of the issue
2. Potential impact (High/Medium/Low)
3. Suggested priority (Urgent/High/Medium/Low)
4. Technical areas that might be affected
5. Recommended next steps

User message: "{message}"

Provide a structured analysis:
"""
            
            analysis_response = self.model.generate_content(analysis_prompt)
            
            # Generate report ID
            existing_reports = [f for f in os.listdir(self.reports_dir) if f.startswith(intent) and f.endswith('.json')]
            report_id = len(existing_reports) + 1
            
            # Create report data
            report = {
                "report_id": f"{intent}_{report_id:04d}",
                "timestamp": datetime.now().isoformat(),
                "type": intent,
                "user_message": message,
                "ai_analysis": analysis_response.text,
                "status": "new",
                "assigned_to": None,
                "email_sent": intent == 'bug'  # Track if email was sent
            }
            
            # Save report to file
            filename = f"{intent}_{report_id:04d}.json"
            filepath = os.path.join(self.reports_dir, filename)
            
            with open(filepath, 'w', encoding='utf-8') as f:
                json.dump(report, f, indent=2, ensure_ascii=False)
            
            print(f"[CHAT] Created developer report: {filename}")
            
            # Also log to a summary file
            self._log_to_summary(intent, message, filename)
            
            return report_id
            
        except Exception as e:
            print(f"[CHAT] Error creating developer report: {e}")
            return None

    def _log_to_summary(self, intent, message, filename):
        """Log to a summary file for quick developer overview"""
        try:
            summary_file = os.path.join(self.reports_dir, "summary.txt")
            timestamp = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
            
            with open(summary_file, 'a', encoding='utf-8') as f:
                f.write(f"[{timestamp}] {intent.upper()}: {message[:100]}... (see {filename})\n")
                
        except Exception as e:
            print(f"[CHAT] Error logging to summary: {e}")

    def get_developer_reports(self, report_type=None):
        """Method for developers to retrieve reports"""
        try:
            reports = []
            for filename in os.listdir(self.reports_dir):
                if filename.endswith('.json'):
                    if report_type and not filename.startswith(report_type):
                        continue
                        
                    with open(os.path.join(self.reports_dir, filename), 'r', encoding='utf-8') as f:
                        report = json.load(f)
                        report['filename'] = filename
                        reports.append(report)
            
            return sorted(reports, key=lambda x: x['timestamp'], reverse=True)
            
        except Exception as e:
            print(f"Error retrieving reports: {e}")
            return []
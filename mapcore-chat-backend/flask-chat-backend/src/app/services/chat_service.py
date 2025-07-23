import os
import json
import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart
from datetime import datetime
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
        
        # Email configuration
        self.smtp_server = os.getenv("SMTP_SERVER", "smtp.gmail.com")
        self.smtp_port = int(os.getenv("SMTP_PORT", "587"))
        self.email_user = os.getenv("EMAIL_USER")
        self.email_password = os.getenv("EMAIL_PASSWORD")
        self.developer_email = os.getenv("DEVELOPER_EMAIL", "tova.barzel@mapcore.com")

    def process_message(self, message):
        if not message:
            return "Please provide a message."

        try:
            # First, classify the message intent
            intent = self._analyze_intent(message)
            print(f"[CHAT] Detected intent: {intent}")
            
            # Report to developers if needed
            if intent in ['bug', 'missing-feature']:
                report_id = self._report_to_developers(message, intent)
                
                # Send email for bugs
                if intent == 'bug':
                    self._send_bug_email(message, report_id)
            
            # Get relevant context from RAG
            context = ""
            if self.rag_service:
                relevant_docs = self.rag_service.search_documents(message)
                context = "\n".join(relevant_docs)
            
            # Create enhanced prompt based on intent
            response = self._generate_response(intent, message, context)
            
            return response
            
        except Exception as e:
            return f"Error processing message: {e}"

    def _send_bug_email(self, user_message, report_id):
        """Send email notification for bug reports"""
        try:
            if not all([self.email_user, self.email_password, self.developer_email]):
                print("[EMAIL] Email configuration missing, skipping email notification")
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

            # Send email
            print(f"[EMAIL] Sending bug report to {self.developer_email}...")
            
            with smtplib.SMTP(self.smtp_server, self.smtp_port) as server:
                server.starttls()
                server.login(self.email_user, self.email_password)
                text = msg.as_string()
                server.sendmail(self.email_user, self.developer_email, text)
            
            print(f"[EMAIL] ✓ Bug report email sent successfully!")
            return True
            
        except Exception as e:
            print(f"[EMAIL] ✗ Failed to send bug report email: {e}")
            return False

    def _report_to_developers(self, message, intent):
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

    def _analyze_intent(self, message):
        """Use AI to classify if message is bug, missing-feature, or regular question"""
        try:
            classification_prompt = f"""
Analyze the following user message and classify it into one of these categories:
- "bug": The user is reporting an error, crash, malfunction, or something not working as expected
- "missing-feature": The user is requesting a new feature, enhancement, or functionality that doesn't exist
- "question": A regular question about how to use existing functionality

Examples:
- "The app crashes when I click submit" → bug
- "Can you add dark mode?" → missing-feature  
- "How do I create a new project?" → question
- "The login button doesn't work" → bug
- "I need export to PDF feature" → missing-feature
- "What's the keyboard shortcut for save?" → question

User message: "{message}"

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
            return 'question'  # Default to question if classification fails

    def _generate_response(self, intent, message, context):
        """Generate appropriate response based on intent"""
        try:
            if intent == 'bug':
                base_response = "I understand you've encountered an issue. I've reported this to our development team for investigation and sent an email notification to our developers."
                
            elif intent == 'missing-feature':
                base_response = "Thank you for the feature suggestion! I've forwarded your request to our development team for consideration."
                
            else:  # regular question
                base_response = ""
            
            # Create context-aware prompt
            if context:
                prompt = f"""Based on the following context, answer the user's question:

Context:
{context}

User's {intent}: {message}

{base_response}

Please provide a helpful response:"""
            else:
                prompt = f"""User's {intent}: {message}

{base_response}

Please provide a helpful response:"""
            
            response = self.model.generate_content(prompt)
            
            # Add developer notification to response
            if intent in ['bug', 'missing-feature']:
                existing_reports = [f for f in os.listdir(self.reports_dir) if f.startswith(intent) and f.endswith('.json')]
                report_num = len(existing_reports)
                
                if intent == 'bug':
                    dev_note = f"\n\n📧 Note: This bug has been automatically reported to our development team and an email notification has been sent to tova.barzel@mapcore.com (Report ID: bug_{report_num:04d})."
                else:
                    dev_note = f"\n\n📝 Note: This feature request has been automatically reported to our development team (Report ID: {intent}_{report_num:04d})."
            else:
                dev_note = ""

            # Detect languages in the user's message
            language_examples = {
                "react": {
                    "label": "React",
                    "title": "Example React Component",
                    "code": (
                        "```jsx\n"
                        "import React from 'react';\n"
                        "\n"
                        "const ExampleComponent = () => (\n"
                        "  <div>\n"
                        "    <h2>Hello from React!</h2>\n"
                        "    <p>This is a sample component.</p>\n"
                        "  </div>\n"
                        ");\n"
                        "\n"
                        "export default ExampleComponent;\n"
                        "```"
                    )
                },
                "c#": {
                    "label": "C#",
                    "title": "Example C# Component",
                    "code": (
                        "```csharp\n"
                        "using System;\n"
                        "\n"
                        "public class ExampleComponent\n"
                        "{\n"
                        "    public void Render()\n"
                        "    {\n"
                        "        Console.WriteLine(\"Hello from C#!\");\n"
                        "    }\n"
                        "}\n"
                        "```"
                    )
                },
                "typescript": {
                    "label": "TypeScript",
                    "title": "Example TypeScript Component",
                    "code": (
                        "```tsx\n"
                        "import React from 'react';\n"
                        "\n"
                        "type Props = { message: string };\n"
                        "\n"
                        "const ExampleComponent: React.FC<Props> = ({ message }) => (\n"
                        "  <div>\n"
                        "    <h2>{message}</h2>\n"
                        "  </div>\n"
                        ");\n"
                        "\n"
                        "export default ExampleComponent;\n"
                        "```"
                    )
                },
                "javascript": {
                    "label": "JavaScript",
                    "title": "Example JavaScript Function",
                    "code": (
                        "```js\n"
                        "function example() {\n"
                        "  console.log('Hello from JavaScript!');\n"
                        "}\n"
                        "example();\n"
                        "```"
                    )
                },
                "python": {
                    "label": "Python",
                    "title": "Example Python Function",
                    "code": (
                        "```python\n"
                        "def example():\n"
                        "    print('Hello from Python!')\n"
                        "\n"
                        "example()\n"
                        "```"
                    )
                }
            }

            # Lowercase message for detection
            msg_lower = message.lower()
            detected = []
            for key in language_examples:
                if key in msg_lower or (key == "c#" and ("csharp" in msg_lower or "c sharp" in msg_lower)):
                    detected.append(key)

            # Default to React and C# if nothing detected
            if not detected:
                detected = ["react", "c#"]

            # Build code examples array
            code_examples = []
            for lang in detected:
                code_examples.append({
                    "language": language_examples[lang]["label"],
                    "example": language_examples[lang]["code"]
                })

            # Format code examples as a Markdown array
            code_examples_md = "\n\n---\n\n**Example Code in Relevant Languages:**\n\n"
            for ex in code_examples:
                code_examples_md += f"**{ex['language']}**\n{ex['example']}\n\n"

            return response.text + dev_note + code_examples_md

        except Exception as e:
            return f"Error generating response: {e}"

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
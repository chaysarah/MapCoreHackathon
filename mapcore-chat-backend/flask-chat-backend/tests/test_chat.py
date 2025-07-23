import unittest
from src.app import create_app
from src.app.services.chat_service import ChatService

class ChatServiceTestCase(unittest.TestCase):
    def setUp(self):
        self.app = create_app()
        self.app_context = self.app.app_context()
        self.app_context.push()
        self.chat_service = ChatService()

    def tearDown(self):
        self.app_context.pop()

    def test_process_message(self):
        response = self.chat_service.process_message("Hello, how are you?")
        self.assertIsInstance(response, str)
        self.assertNotEqual(response, "")

    def test_chat_route(self):
        with self.app.test_client() as client:
            response = client.post('/chat', json={'message': 'Hello!'})
            self.assertEqual(response.status_code, 200)
            self.assertIn('response', response.get_json())

if __name__ == '__main__':
    unittest.main()
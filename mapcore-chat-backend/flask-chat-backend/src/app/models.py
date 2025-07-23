class Message:
    def __init__(self, user_id, content):
        self.user_id = user_id
        self.content = content

class User:
    def __init__(self, user_id, username):
        self.user_id = user_id
        self.username = username

class Chat:
    def __init__(self):
        self.messages = []

    def add_message(self, message):
        self.messages.append(message)

    def get_messages(self):
        return self.messages
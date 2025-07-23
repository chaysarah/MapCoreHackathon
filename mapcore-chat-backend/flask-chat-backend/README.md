# Flask Chat Backend

This project is a simple chat backend implemented using Flask. It provides endpoints for receiving chat messages, processing them, and responding with answers.

## Project Structure

```
flask-chat-backend
├── src
│   └── app
│       ├── __init__.py
│       ├── routes.py
│       ├── services
│       │   └── chat_service.py
│       └── models.py
├── tests
│   └── test_chat.py
├── requirements.txt
├── config.py
└── README.md
```

## Installation

1. Clone the repository:
   ```
   git clone <repository-url>
   cd flask-chat-backend
   ```

2. Create a virtual environment:
   ```
   python -m venv venv
   ```

3. Activate the virtual environment:
   - On Windows:
     ```
     venv\Scripts\activate
     ```
   - On macOS/Linux:
     ```
     source venv/bin/activate
     ```

4. Install the required packages:
   ```
   pip install -r requirements.txt
   ```

## Usage

1. Set up the configuration in `config.py` as needed.
2. Run the application:
   ```
   python -m src.app
   ```

3. Send a chat message to the endpoint defined in `routes.py` to receive a response.

## Testing

To run the tests, use the following command:
```
pytest tests/test_chat.py
```

## Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.
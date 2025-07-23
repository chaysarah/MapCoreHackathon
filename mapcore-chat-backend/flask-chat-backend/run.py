from src.app import create_app

app = create_app()

if __name__ == '__main__':
    # bind to 0.0.0.0 if you want to test from another device
    app.run(host='127.0.0.1', port=5000, debug=True)
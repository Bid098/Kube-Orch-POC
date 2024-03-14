from flask import Flask, jsonify
import random

app = Flask(__name__)

@app.route('/data')
def get_data():
    # Genera un valore casuale
    value = random.randint(0, 100)
    return jsonify({'value': value})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)

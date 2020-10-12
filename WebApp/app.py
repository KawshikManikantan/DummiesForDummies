from flask import Flask, render_template, request
import os
import subprocess

app = Flask(__name__)

@app.route('/')
@app.route('/home')
def home():
    return render_template('index.html')


@app.route('/editor', methods=['GET', 'POST'])
def algo1():
    __location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))
    if request.method == 'POST':
        try:
           user = request.form['code']
           f = open(os.path.join(__location__, 'code1.cpp'), "w")
           f.write(user)
           f.close()
           print(user)
        except:
           print("No Result obtained")
        finally:
           return render_template('editor.html')
    else:
        return render_template('editor.html')


if __name__ == '__main__':
    app.run(debug=True)

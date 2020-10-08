from flask import Flask, render_template, request
import os
import subprocess

app = Flask(__name__)


@app.route('/', methods=['GET', 'POST'])
def algo1():
    __location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))
    if request.method == 'POST':
        try:
           user = request.form['code']
           f = open(os.path.join(__location__, 'code1.cpp'), "w")
           f.write(user)
           f.close()
           # create a pipe to a child process
           data, temp = os.pipe()

           # write to STDIN as a byte object(convert string
           # to bytes with encoding utf8)
           os.write(temp, bytes("5 10\n", "utf-8"));
           os.close(temp)

           # store output of the program as a byte string in s
           s = subprocess.check_output("cd "+'"'+__location__+'"'+g++ HelloWorld.cpp -o out2;./out2",
                                       stdin=data, shell=True)

           # decode s to a normal string
           print(s.decode("utf-8"))
           # s = subprocess.check_call("gcc.c -o out1;./out1",shell=True)
           # subprocess.call(["cd ",__location__])
           # subprocess.call(["pwd"])
           # subprocess.call(["g++ code1.cpp -o code1"])
           # subprocess.call("code1")
           print(user)
        except:
           print("No Result obtained")
        finally:
           return render_template('hello.html')
    else:
        return render_template('hello.html')


if __name__ == '__main__':
    app.run(debug=True)

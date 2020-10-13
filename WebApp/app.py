from flask import Flask, render_template, request
import os
import subprocess

app = Flask(__name__)


@app.route('/', methods=['GET', 'POST'])
def algo1():
    __location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))
    output_display=[];
    if request.method == 'POST':
        try:
           user = request.form['code']
           f = open(os.path.join(__location__, 'code1.cpp'), "w")
           f.write(user)
           f.close()

           input_array = []
           with open(os.path.join(__location__, 'test_cases.txt'), "r") as f:
               for line in f:
                   input_array.append(line)
           f.close()

           correct_sol = []
           with open(os.path.join(__location__, 'cor_output.txt'), "r") as f:
               for line in f:
                   correct_sol.append(line.strip())
           f.close()

           submitted_sol = []

           num_input = 2
           i = 0

           try:
               s = subprocess.check_output("g++ code1.cpp -o out2",cwd=__location__,timeout=1);
               print("Hey" + s.decode("utf-8"))
           except:
               # output_display.append(s.decode("utf-8"))

               return

           data, temp = os.pipe()
           while i < len(input_array):
               # print(input_array[i])
               inputstr = ""
               for j in range(num_input):
                   inputstr = inputstr + input_array[i]
                   i += 1
               print(inputstr)
               os.write(temp, bytes(inputstr, "utf-8"))
               if i == len(input_array):
                   os.close(temp)
               s = subprocess.check_output("out2",cwd=__location__,
                   stdin=data,timeout=1)
               print(s.decode("utf-8"))
               submitted_sol.append((s.decode("utf-8")).strip())
           flag = 0
           j = 0
           # for i in submitted_sol:
           #         print(i)
           #
           # for i in correct_sol:
           #         print(i)
           print(len(submitted_sol))
           print(len(correct_sol))
           if len(submitted_sol) != len(correct_sol):
               print("Extra Characters Printed")
           else:
               while j < len(submitted_sol):
                   if submitted_sol[j] != correct_sol[j]:
                       # print(len(submitted_sol[j]), submitted_sol[j],
                       #       len(correct_sol[j]), correct_sol[j])
                       flag = 1
                       break
                   j += 1
               if flag == 0:
                   print("Congrats All testcases passed")
               else:
                   print("The testcase " + str(j) + " failed")
        except subprocess.CalledProcessError as e:
           print("No Result obtained")
           print(e.output)
        finally:
           return render_template('hello.html')
    else:
        return render_template('hello.html')


if __name__ == '__main__':
    app.run(debug=True)

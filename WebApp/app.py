from flask import Flask, render_template, request, redirect
import os
import subprocess

app = Flask(__name__)
filenames = ("SOR_question.html", "BS_question.html", "BST_question.html", 
            "BFS_question.html", "DFS_question.html", "KRU_question.html")

def testhelp():
    if id == "1":
        return render_template('BS_question.html')
    elif id == "2":
        return render_template("BST_question.html")
    elif id == "3":
        return render_template("BFS_question.html")
    elif id == "4":
        return render_template("DFS_question.html")
    elif id== "5":
        return render_template("KRU_question.html")
    elif id== "0":
        return render_template("SOR_question.html")


@app.route('/')
def frontPage():
    return render_template("frontpage.html")


@app.route('/home')
def home():
    return render_template('index.html')


@app.route('/games')
def game():
    return render_template('games.html')


@app.route("/coding")
def coding():
    return render_template("coding.html")


@app.route('/intro')
def intro():
    return render_template('content.html')


@app.route('/game/<id>')
def gamesTab(id):
    if id == "1":
        return render_template("BS_game.html")
    elif id == "2":
        return render_template("BST_game.html")
    elif id == "3":
        return render_template("BFS-DFS_game.html")
    elif id == "4":
        return render_template("BFS-DFS_game.html")
    elif id == "5":
        return render_template("KRU_game.html")
    elif id == "0":
        return render_template("SOR_game.html")
    else:
        return render_template("games_layout.html")

@app.route('/content/<idd>')
def contentTab(idd):
    if idd == "1":
        return render_template("BS_content.html")
    elif idd == "2":
        return render_template("BST_content.html")
    elif idd == "3":
        return render_template("BFS_content.html")
    elif idd == "4":
        return render_template("DFS_content.html")
    elif idd == "5":
        # print("Eh?")
        return render_template("KRU_content.html")
    elif idd == "0":
        return render_template("SOR_content.html")
    else:
        return render_template("games_layout.html")

@app.route('/edi/<id>', methods=['GET', 'POST'])
def algo(id):
    if request.method == 'POST':
        return algo_init('code1.cpp', 'test_cases.txt', 'cor_output.txt',
                         'out2', 2, int(id))
    else:
        if id == "1":
            return render_template("BS_question.html")
        elif id == "2":
            return render_template("BST_question.html")
        elif id == "3":
            return render_template("BFS_question.html")
        elif id == "4":
            return render_template("DFS_question.html")
        elif id == "5":
            return render_template("KRU_question.html")
        elif id == "0":
            return render_template("SOR_question.html")
        else:
            return render_template("questions_layout.html")


def algo_init(filetowritecode, filetestcases, filetooutput, filetoexecute,
              num_input, id):
    __location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))
    output_display = []
    issuccess = 1
    user = request.form['code']
    # user=[]
    # for ll in users:
    #     if(ll.st):
    #         user.appen
    try:
        f = open(os.path.join(__location__, filetowritecode), "w")
        f.write(user)
        f.close()

        input_array = []
        with open(os.path.join(__location__, filetestcases), "r") as f:
            for line in f:
                input_array.append(line)
        f.close()

        correct_sol = []
        with open(os.path.join(__location__, filetooutput), "r") as f:
            for line in f:
                correct_sol.append(line.strip())
        f.close()

        submitted_sol = []

        # num_input = 2
        i = 0

    except:
        issuccess = 0
        print("Enters Except")
        output_display.append("System Error: Please try again")
        return render_template(filenames[id], code=user,
                               output=output_display)
    else:
        print(issuccess)
        try:
            command="g++ " + filetowritecode + " -o " + filetoexecute
            print(command)
            s = subprocess.check_output(command,
                cwd=__location__, timeout=1,
                stderr=subprocess.STDOUT)
        except Exception as e:
            print("Entered Here 1")
            instr = e.output.decode()
            for i in instr.split('\n'):
                output_display.append(i)
            if len(instr) != 0:
                return render_template(filenames[id], code=user,
                                       output=output_display)
        data, temp = os.pipe()
        i=0
        while i < len(input_array):
            print(i)
            inputstr = ""
            # for j in range(num_input):
            while i<len(input_array) and input_array[i]!="\n" and len(input_array) > 0:
                inputstr = inputstr + input_array[i]
                i += 1
            i+=1
            print(inputstr)
            os.write(temp, bytes(inputstr, "utf-8"))
            if i == len(input_array):
                os.close(temp)
            try:
                s = subprocess.check_output(filetoexecute,
                                            cwd=__location__,
                                            stdin=data, timeout=1,
                                            stderr=subprocess.STDOUT)
            except subprocess.TimeoutExpired as e:
                print("No Result obtained")
                print(e.output)
                output_display.append("TimedOut/Segmentation Fault")
                return render_template(filenames[id], code=user,
                                       output=output_display)
            else:
                print(s.decode("utf-8"))
                submitted_sol.append((s.decode("utf-8")).strip())
        flag = 0
        j = 0
        print(len(submitted_sol))
        print(len(correct_sol))
        if len(submitted_sol) != len(correct_sol):
            print("Extra Characters Printed")
        else:
            while j < len(submitted_sol):
                if submitted_sol[j] != correct_sol[j]:
                    flag = 1
                    break
                j += 1
            if flag == 0:
                print("Congrats All testcases passed")
                output_display.append("Congrats All testcases passed")

            else:
                print("The testcase " + str(j) + " failed")
                output_display.append(
                    "The testcase " + str(j) + " failed")
        for result in output_display:
            print(result)
        return render_template(filenames[id], code=user
                               , output=output_display)


@app.route('/test')
def test(id):
    return testhelp(id)


if __name__ == '__main__':
    app.run()

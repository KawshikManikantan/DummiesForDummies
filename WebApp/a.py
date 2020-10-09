import os
import subprocess

__location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))

print(__location__)

input_array=[]
with open(os.path.join(__location__, 'test_cases.txt'), "r") as f:
        for line in f:
                input_array.append(line)
f.close()

correct_sol=[]
with open(os.path.join(__location__, 'cor_output.txt'), "r") as f:
        for line in f:
                correct_sol.append(line.strip())
f.close()

submitted_sol=[]

num_input=2
i=0
data, temp = os.pipe()
print(len(input_array))
while i<len(input_array):
        # print(input_array[i])
        inputstr=""
        for j in range(num_input):
                inputstr=inputstr+input_array[i]
                i+=1
        print(inputstr)
        os.write(temp, bytes(inputstr, "utf-8"))
        if i==len(input_array):
                os.close(temp)
        s = subprocess.check_output("cd "+'"'+__location__+'"'+ "&&" + "g++ a.cpp -o out2" + " && out2", stdin=data, shell=True)
        print(s.decode("utf-8"))
        submitted_sol.append((s.decode("utf-8")).strip())
flag=0
j=0
# for i in submitted_sol:
#         print(i)
#
# for i in correct_sol:
#         print(i)
print(len(submitted_sol))
print(len(correct_sol))
if len(submitted_sol)!=len(correct_sol):
        print("Extra Characters Printed")
else:
        while j<len(submitted_sol):
                if submitted_sol[j]!=correct_sol[j]:
                        print(len(submitted_sol[j]),submitted_sol[j],len(correct_sol[j]),correct_sol[j])
                        flag=1
                        break
                j+=1
        if flag==0:
                print("Congrats All testcases passed")
        else:
                print("The testcase "+ str(j) +" failed")

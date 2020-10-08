import os
import subprocess
__location__ = os.path.realpath(
        os.path.join(os.getcwd(), os.path.dirname(__file__)))
print(__location__)

# create a pipe to a child process
data, temp = os.pipe()

# write to STDIN as a byte object(convert string
# to bytes with encoding utf8)
os.write(temp, bytes("5 10\n", "utf-8"))
os.close(temp)

# store output of the program as a byte string in s
print("cd "+'"'+__location__+'"'+ "&&" + "g++ a.cpp -o out2" + " && out2")
s = subprocess.check_output("cd "+'"'+__location__+'"'+ "&&" + "g++ a.cpp -o out2" + " && out2", stdin=data, shell=True)

# decode s to a normal string
print(s.decode("utf-8"))
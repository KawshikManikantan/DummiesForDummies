using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BST : MonoBehaviour
{
    public GameObject Header;
    public GameObject nodePrefab;
    private GameObject[] nodes;
    private GameObject[] lines;
    private int num_lines;
    private int[] numbers;
    public GameObject Footer;
    public GameObject input;
    public GameObject insert_btn, search_btn, start_btn, end_btn, left_btn, right_btn, found_btn;
    private int option, index, curr_num, choice, correct;
    private string curr;
    private float length_y = 1f, length_x = 3.6f;
    private bool operation_start;
    public GameObject line; 
    private Vector3 root;
    private int numNodes;
    private Vector3 normalScale;
    private Vector3 activeScale;
    // Start is called before the first frame update
    void Start()
    {
        Header.GetComponent<Text>().text = "Binary Search Tree(BST)";
        input.SetActive(false);
        num_lines = 0;
        normalScale = new Vector3(0.07f, 0.07f, 1);
        activeScale = normalScale + new Vector3(0.02f, 0.02f, 1);
        start_btn.SetActive(false);
        end_btn.SetActive(false);
        left_btn.SetActive(false);
        right_btn.SetActive(false);
        found_btn.SetActive(false);
        Footer.GetComponent<Text>().text = "";
        option = -1;
        index = 0;
        choice = -1;
        numNodes = 0;
        operation_start = false;
        nodes = new GameObject[17];
        lines = new GameObject[15];
        numbers = new int[17];
        for(int i=1;i<=16;i++)
            numbers[i] = -1;
        root = new Vector3(11.81f, 10.55f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(option==0)
            insertGame();
        else if(option==1)
            searchGame();
    }
    private void insertNode()
    {
        numbers[index] = curr_num;
        float dec = 1.53f;
        if(index==1)
            nodes[index] = Instantiate(nodePrefab, root, Quaternion.identity);
        else
        {
            int num=0;
            while(index/(1<<num)!=1)
                num++;
            num = num-1;
            Vector3 displacement;
            if(index%2==0)
                displacement = new Vector3(-length_x + num * dec, -length_y, 0);
            else
                displacement = new Vector3(length_x - num * dec, -length_y, 0);
            nodes[index] = Instantiate(nodePrefab, nodes[index/2].transform.position + displacement, Quaternion.identity);
            GameObject newLineObj = Instantiate(line, new Vector3(0,0,0), Quaternion.identity);
            LineRenderer newLine = newLineObj.GetComponent<LineRenderer>();
            newLine.SetPosition(0, nodes[index].transform.position);
            newLine.SetPosition(1, nodes[index/2].transform.position);
            lines[num_lines++] = newLineObj;
        }
        GameObject child = nodes[index].transform.GetChild(0).gameObject;
        child.GetComponent<TextMesh>().text = curr;
        operationEnd();
        numNodes++;
        index = 1;
        choice = -1;
    }
    private void operationEnd()
    {
        operation_start = false;
        left_btn.SetActive(false);
        right_btn.SetActive(false);
        found_btn.SetActive(false);
        start_btn.SetActive(true);
        end_btn.SetActive(true);
        if(option==1 && numbers[index]!=-1)
            nodes[index].transform.localScale = activeScale;
        if(Footer.GetComponent<Text>().text == "")
            Footer.GetComponent<Text>().text = "Enter a number to be inserted";
    }
    private void operationStart()
    {
        operation_start = true;
        start_btn.SetActive(false);
        end_btn.SetActive(false);
        left_btn.SetActive(true);
        right_btn.SetActive(true);
        if(option==1)
            found_btn.SetActive(true);
    }
    private void insertGame()
    {
        for(int i=1;i<16;i++)
            if(numbers[i]!=-1)
                nodes[i].transform.localScale = normalScale;
        if(operation_start)
        {
            if(index>=16)
            {
                Footer.GetComponent<Text>().text = "Sorry, the node can't be inserted(height will become 4)";
                operationEnd();
            }
            else if(numbers[index]!=-1)
                nodes[index].transform.localScale = activeScale;
            else if(numbers[index]==-1)
            {
                insertNode();
                return;
            }
            if(curr_num==numbers[index])
            {
                Footer.GetComponent<Text>().text = "Number already present";
                operationEnd();
                return ;
            }
            else if(curr_num<numbers[index])
                correct = 0;
            else
                correct = 1;
            if(choice==-1)
                return;
            if(choice == correct)
            {
                index = 2*index + correct;
                choice = -1;
                Footer.GetComponent<Text>().text = "";
            }
            else
            {
                Footer.GetComponent<Text>().text = "Wrong Choice";
                choice = -1;
            }
        }
    }
    private void searchGame()
    {
        if(operation_start)
        {
            for(int i=1;i<=15;i++)
                if(numbers[i]!=-1)
                    nodes[i].transform.localScale = normalScale;
            int correct=-1;
            if(index>15 || numbers[index]==-1)
            {
                Footer.GetComponent<Text>().text = "Element not found";
                operationEnd();
                return;
            }
            if(numbers[index]!=-1)
                nodes[index].transform.localScale = activeScale;
            if(curr_num==numbers[index])
                correct = 2;
            else if(curr_num>numbers[index])
                correct = 1;
            else
                correct = 0;
            if(choice==-1)
                return;
            if(choice==correct)
            {
                Footer.GetComponent<Text>().text = "";
                if(correct!=2)
                    index = 2*index + correct;
                else
                {
                    Footer.GetComponent<Text>().text = "Element Found";
                    operationEnd();
                }
                choice = -1;
            }
            else
            {
                Footer.GetComponent<Text>().text = "Wrong Choice";
                choice = -1;
            }
        }
    }
    private int getNumber(string text)
    {
        int length = text.Length;
        int num = 0;
        for(int i=0;i<length;i++)
        {
            if(text[i]<'0' || text[i]>'9')
                return -1;
            num = num*10 + (text[i] - '0');
        }
        return num;
    }
    public void start_btn_op()
    {
        if(option==0 && numNodes>=15)
        {
            Footer.GetComponent<Text>().text = "Cannot insert any more nodes";
            return;
        }
        GameObject textObj = input.transform.GetChild(2).gameObject;
        string str = textObj.GetComponent<Text>().text.Trim();
        curr_num = getNumber(str);
        if(curr_num==-1)
        {
            Footer.GetComponent<Text>().text = "Not a valid number";
            return ;
        }
        if(curr_num<0 || curr_num>100)
        {
            Footer.GetComponent<Text>().text = "Not in range(0 - 100)";
            return ;
        }
        operation_start = true;
        curr = str;
        Footer.GetComponent<Text>().text = "";
        index = 1;
        operationStart();
    }
    public void end_btn_op()
    {
        option = -1;
        Header.GetComponent<Text>().text = "Binary Search Tree(BST)";
        insert_btn.SetActive(true);
        search_btn.SetActive(true);
        input.SetActive(false);
        start_btn.SetActive(false);
        end_btn.SetActive(false);
        Footer.GetComponent<Text>().text = "";
        for(int i=1;i<=16;i++)
            if(numbers[i]!=-1)
                nodes[i].SetActive(false);
        for(int i=0;i<num_lines;i++)
            lines[i].SetActive(false);
    }
    public void left_btn_op()
    {
        choice = 0;
    }
    public void right_btn_op()
    {
        choice = 1;
    }
    public void found_btn_op()
    {
        choice = 2;
    }
    public void insert_btn_op()
    {
        for(int i=1;i<=16;i++)
            if(numbers[i]!=-1)
                nodes[i].SetActive(true);
        for(int i=0;i<num_lines;i++)
            lines[i].SetActive(true);
        option = 0;
        Header.GetComponent<Text>().text = "BST(Insert)";
        insert_btn.SetActive(false);
        search_btn.SetActive(false);
        input.SetActive(true);
        start_btn.SetActive(true);
        end_btn.SetActive(true);
        Footer.GetComponent<Text>().text = "Enter the number to be inserted";
    }
    public void search_btn_op()
    {
        if(numNodes==0)
        {
            Footer.GetComponent<Text>().text = "Please insert some nodes first";
            return;
        }
        for(int i=1;i<=16;i++)
            if(numbers[i]!=-1)
                nodes[i].SetActive(true);
        for(int i=0;i<num_lines;i++)
            lines[i].SetActive(true);
        option = 1;
        Header.GetComponent<Text>().text = "BST(Search)";
        insert_btn.SetActive(false);
        search_btn.SetActive(false);
        input.SetActive(true);
        start_btn.SetActive(true);
        end_btn.SetActive(true);
        Footer.GetComponent<Text>().text = "Enter the number to be searched";
    }
}

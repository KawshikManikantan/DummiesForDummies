using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    private GameObject[] nodes;
    private int[] nums;
    private int toSearch;
    public GameObject nodePrefab;
    public GameObject header;
    public GameObject footer;
    public GameObject message;
    public GameObject input;
    public GameObject add_btn;
    public GameObject start_btn;
    public GameObject left_btn;
    public GameObject right_btn;
    public GameObject found_btn;
    private bool finished;
    private int num_nodes;
    private int started;
    private float speed;
    private int s, e, m, option;
    // Start is called before the first frame update
    void Start()
    {
        num_nodes = 0;
        option = -1;
        speed = 0.06f;
        s = 0; e = 0; m=(s+e)/2;
        nodes = new GameObject[10];
        nums = new int[10];
        started = 0;
        finished = false;
        header.GetComponent<Text>().text = "Binary Search";
        message.GetComponent<Text>().text = "";
        footer.GetComponent<Text>().text = "";
        left_btn.SetActive(false);
        right_btn.SetActive(false);
        found_btn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(finished==true)
        {
            header.GetComponent<Text>().text = "Algorithm Completed";
            message.GetComponent<Text>().text = "";
            for(int i=0;i<num_nodes;i++)
                nodes[i].SetActive(false);
            if(s<=e)
            {
                nodes[m].SetActive(true);
                nodes[m].transform.position = new Vector3(6.5f, 8.5f, 0);
                footer.GetComponent<Text>().text = "Element Found";
            }
            else
                footer.GetComponent<Text>().text = "Element not found";
            left_btn.SetActive(false);
            right_btn.SetActive(false);
            found_btn.SetActive(false);
        }
        else if(started==1)
            moveNodes();
        else if(started==2)
        {
            playGame();
        }
    }

    public void add_node()
    {
        if(num_nodes>=7)
        {
            footer.GetComponent<Text>().text = "Limit reached";
            return ;
        }
        GameObject textObj = input.transform.GetChild(2).gameObject;
        string text = textObj.GetComponent<Text>().text.Trim();
        int length = text.Length, num=0;
        if(length == 0)
        {
            footer.GetComponent<Text>().text = "Enter some number";
            return ;
        }
        string assign = text;
        for(int i=0; i<length; i++)
        {
            if(text[i]<'0' || text[i]>'9')
            {
                footer.GetComponent<Text>().text = "Not a valid number";
                return ;
            }
            num=num*10 + (text[i]-'0');
        }
        if(num<0 || num>100)
        {
            footer.GetComponent<Text>().text = "Not in range(0-100)";
            return;
        }
        footer.GetComponent<Text>().text = "";
        GameObject new_node = Instantiate(nodePrefab, new Vector3(6.5f + num_nodes * 1.8f , 8.5f, 0), Quaternion.identity);
        GameObject child = new_node.transform.GetChild(0).gameObject;
        child.GetComponent<TextMesh>().text = assign;
        nodes[num_nodes] = new_node;
        nums[num_nodes] = num;
        num_nodes++;
    }

    private void playGame()
    {
        int correct = -1;
        header.GetComponent<Text>().text = "Binary Search";
        int n = e-s+1;
        if(n<=0)
        {
            finished = true;
            return ;
        }
        int temp_s = s, temp_e = e, temp_m = (s+e)/2;
        for(int i=0;i<s;i++)
            nodes[i].SetActive(false);
        for(int i=e+1;i<num_nodes;i++)
            nodes[i].SetActive(false);
        for(int i=0; i<n ; i++)
            nodes[s + i].transform.position = new Vector3(6.5f + i * 1.8f , 8.5f, 0);
        nodes[temp_m].transform.localScale = new Vector3 (0.16f,0.16f, 1);
        if(nums[temp_m]>toSearch)
        {
            temp_e = temp_m-1;
            correct = 0;
        }
        else if(nums[temp_m]<toSearch)
        {
            temp_s = temp_m+1;
            correct = 1;
        }
        else
            correct = 2;
        if(option==-1)
            return;
        if(option==correct)
        {
            footer.GetComponent<Text>().text = "";
            s = temp_s;
            e = temp_e;
            m = (s+e)/2;
            if(correct==2)
                finished = true;
        }
        else
            footer.GetComponent<Text>().text = "Wrong Choice";
        option = -1;
    }

    private void moveNodes()
    {
        bool check = true;
        Vector3 final_position;
        Vector3 velocity;
        for(int i=0;i<num_nodes;i++)
        {
            final_position = new Vector3(6.5f + i*1.8f, 8.5f, 0);
            velocity = final_position - nodes[i].transform.position;
            if(velocity.magnitude>0.001f)
            {
                nodes[i].transform.position += velocity.normalized * speed;
                check = false;
            }
        }
        if(check)
            header.GetComponent<Text>().text = "Sorted the array in ascending order";
        else
            header.GetComponent<Text>().text = "Sorting the array in ascending order";
    }

    public void on_start()
    {
        if(started==0)
        {
            if(num_nodes>0)
            {
                add_btn.SetActive(false);
                e = num_nodes - 1;
                started = 1;
                footer.GetComponent<Text>().text = "Enter the element to be searched";
                for(int i=0;i<num_nodes;i++)
                {
                    for(int j=0;j<num_nodes-1-i;j++)
                    {
                        if(nums[j]>nums[j+1])
                        {
                            int temp = nums[j];
                            nums[j] = nums[j+1];
                            nums[j+1] = temp;
                            GameObject store = nodes[j];
                            nodes[j] = nodes[j+1];
                            nodes[j+1] = store;
                        }
                    }
                }
            }
        }
        else
        {
            GameObject numEntered = input.transform.GetChild(2).gameObject;
            string entered = numEntered.GetComponent<Text>().text.Trim();
            int length = entered.Length;
            if(length==0)
            {
                footer.GetComponent<Text>().text = "No number entered";
                return;
            }
            toSearch = 0;
            for(int i=0;i<length;i++)
            {
                if(entered[i]<'0' || entered[i]>'9')
                {
                    message.GetComponent<Text>().text = "Not a valid number";
                    return;
                }
                toSearch = toSearch*10 + (entered[i]-'0');
            }
            started = 2;
            message.GetComponent<Text>().text = "Element to be searched = " + entered;
            footer.GetComponent<Text>().text = "";
            start_btn.SetActive(false);
            input.SetActive(false);
            left_btn.SetActive(true);
            right_btn.SetActive(true);
            found_btn.SetActive(true);
        }
    }

    public void left_btn_action()
    {
        option = 0;
    }

    public void right_btn_action()
    {
        option = 1;
    }

    public void found_btn_action()
    {
        option = 2;
    }
}

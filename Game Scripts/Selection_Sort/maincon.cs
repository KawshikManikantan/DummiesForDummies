using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maincon : MonoBehaviour
{
	public GameObject inputField;
	public GameObject myPrefab;
	public GameObject instructionText;
	public GameObject bottomText;
	bool isStart, cur_select;
	Text inst, bott;
	int add_count;
	int[] mynum;
	int curpos;
	float select_pos;

	void Start()
	{
		inst = instructionText.GetComponent<Text>();
		bott = bottomText.GetComponent<Text>();
		inst.text = "Selection Sort Algorithm";
		bott.text = "";
		isStart = false;
		mynum = new int [0];
		add_count = 0;
		curpos = 0;
		cur_select = false;
		select_pos = -1f;
	}

    public void add_node()
    {
    	string node_val = inputField.GetComponent<Text>().text;
    	if(isStart)
    	{
    		return;
    	}
		if(node_val.Equals(""))
		{
			bott.text = "No number has been entered";
			return;
		}
		if(add_count >= 8)
		{
			bott.text = "Maximum number of nodes reached";
			return;
		}
    	int num = Int16.Parse(node_val);
    	if(num >= 0 && num < 100)
    	{
    		bott.text = "";
    		int[] tempnum = new int[mynum.Length + 1];
    		for(int i = 0;i < mynum.Length;i ++)
    		{
	    		tempnum[i] = mynum[i];
    		}
    		tempnum[mynum.Length] = num;
    		mynum = tempnum;

    		GameObject new_node = Instantiate(myPrefab, new Vector3(6.5f + add_count * 1.5f , 9, 0), Quaternion.identity);
	        GameObject child_text = new_node.transform.GetChild(0).gameObject;
	        var texte = child_text.GetComponent<TextMesh>();
	        texte.text = node_val;
    		add_count ++;
	    }
	    else
	    {
	    	bott.text = "Invalid Range";
	    }
    }

    public void click_node(string str, float inp_select_pos)
    {
    	if(isStart)
    	{
	    	int num = Int16.Parse(str);
	    	if(mynum[curpos] == num)
	    	{
	    		curpos ++;
	    		select_pos = inp_select_pos;
	    		if(curpos < mynum.Length)
	    		{
		    		bott.text = "Smallest node swapped, now repeat the process for selectable nodes";
		    	}
		    	else
		    	{
		    		inst.text = "Algorithm complete, nodes are now sorted!";
		    		bott.text = "";
		    	}
	    		cur_select = true;
	    	}
	    	else
	    	{
	    		bott.text = "Not the smallest node";
	    		cur_select = false;
	    	}
    	}
    }

    public float get_select_pos()
    {
    	cur_select = false;
    	return select_pos;
    }

    public int make_select()
    {
    	if(cur_select == true)
    	{
    		return curpos - 1;
    	}
    	else
    	{
    		return -1;
    	}
    }

    public void on_start()
    {
    	if(!isStart)
    	{
	    	inst.text = "Algorithm : select smallest node which is selectable";
	    	bott.text = "";
	    	isStart = true;
		    Array.Sort(mynum);
		    GameObject but = GameObject.Find("Button_add");
		    but.SetActive(false);
		    but = GameObject.Find("Button_start");
		    but.SetActive(false);
		    but = GameObject.Find("InputField");
		    but.SetActive(false);
	    }
    }
}

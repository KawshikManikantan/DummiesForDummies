using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bfs_dfs_game_controller : MonoBehaviour
{
	public GameObject background;
	public GameObject textParentID;
	public GameObject nodePrefab;
	public GameObject playerObj;
	public GameObject pizzaObj;
	public GameObject but_bfs;
	public GameObject but_dfs;
	public GameObject topHeading;
	public GameObject instructionHeading;
	public Sprite backg;
	bool didStart, gameOver, clickStart;
	int[] parent_of;
	float[,] node_pos;
	int gameMode;//0 : BFS, 1 : DFS

	Queue <int> bfs_ds;
	Stack <int> dfs_ds;

    void Start()
    {
    	didStart = false;
    	gameOver = false;
    	clickStart = false;
    	parent_of = new int[0];
    	playerObj.SetActive(false);
    	pizzaObj.SetActive(false);
	    but_bfs.SetActive(false);
	    but_dfs.SetActive(false);
	    gameMode = -1;
	    topHeading.GetComponent<Text>().text = "DFS/BFS Algorithm for Trees";
	    instructionHeading.GetComponent<Text>().text = "Root ID is -1";
    }

    public void click_add()
    {
    	string parentID = textParentID.GetComponent<Text>().text;
    	int intID;
    	if(!Int32.TryParse(parentID,out intID))
    	{
    		instructionHeading.GetComponent<Text>().text = "Not a number!";
    		return;
    	}
    	if(!((parent_of.Length == 0 && intID == -1) || (intID >= 0 && intID < parent_of.Length)))
    	{
    		if(parent_of.Length == 0 && intID != -1)
    		{
    			instructionHeading.GetComponent<Text>().text = "Parent should be root, ID is -1";
    		}
    		else if(parent_of.Length > 0 && intID == -1)
    		{
    			instructionHeading.GetComponent<Text>().text = "Root can have only one child";
    		}
    		else
    		{
	    		instructionHeading.GetComponent<Text>().text = "Invalid parent ID";
    		}
    		return;
    	}
    	if(get_capacity(intID) == 2)
    	{
    		instructionHeading.GetComponent<Text>().text = "Maximum capacity for parent node reached";
    		return;
    	}
    	if(get_level(intID) == 4)
    	{
    		instructionHeading.GetComponent<Text>().text = "Maximum level for parent node reached";
    		return;
    	}
    	if(parent_of.Length == 20)
    	{
    		instructionHeading.GetComponent<Text>().text = "Maximum number of nodes reached";
    		return;
    	}
    	node_add(intID);
    	float spawn_x = -9.5f + parent_of.Length * 2.1f;
    	float spawn_y = 1.5f;
    	if(parent_of.Length >= 9)
    	{
    		spawn_x = -9.5f + (parent_of.Length - 8) * 2.1f;
    		spawn_y = -1f;
    	}
    	GameObject curNode = Instantiate(nodePrefab, new Vector3(spawn_x, spawn_y, 0f), Quaternion.identity);
    	GameObject childmyID = curNode.transform.GetChild(0).gameObject;
    	GameObject childparID = curNode.transform.GetChild(1).gameObject;
    	childmyID.GetComponent<TextMesh>().text = (parent_of.Length - 1) + "";
    	childparID.GetComponent<TextMesh>().text = parentID;
    	instructionHeading.GetComponent<Text>().text = "Node added";
    }

    int get_capacity(int parentID)
    {
    	int capacity = 0;
    	for(int i = 0;i < parent_of.Length;i ++)
    	{
    		if(parent_of[i] == parentID)
    		capacity ++;
    	}
    	return capacity;
    }

    int get_level(int parentID)
    {
    	int level = 0;
    	while(parentID != -1)
    	{
    		level ++;
    		parentID = parent_of[parentID];
    	}
    	return level;
    }

    void node_add(int parentID)
    {
    	int curLen = parent_of.Length;
    	int[] temp = new int[curLen + 1];
    	for(int i = 0;i < curLen;i ++)
    	{
    		temp[i] = parent_of[i];
    	}
    	temp[curLen] = parentID;
    	parent_of = temp;
    }

    public void click_start()
    {
    	if(parent_of.Length == 0)
    	{
    		instructionHeading.GetComponent<Text>().text = "No nodes added!";
    		return;
    	}
    	clickStart = true;
    	GameObject but = GameObject.Find("Button_add");
	    but.SetActive(false);
	    but = GameObject.Find("Button_start");
	    but.SetActive(false);
	    but = GameObject.Find("InputField ID");
	    but.SetActive(false);
	    but = GameObject.Find("Parent id");
	    but.SetActive(false);
	    but_bfs.SetActive(true);
	    but_dfs.SetActive(true);
	    topHeading.GetComponent<Text>().text = "Choose BFS or DFS";
	    instructionHeading.GetComponent<Text>().text = "";
    }

    public void click_bfs()
    {
    	gameMode = 0;
    	instructionHeading.GetComponent<Text>().text = "Start selecting nodes in BFS manner";
    	topHeading.GetComponent<Text>().text = "BFS Algorithm";
	    bfs_ds = new Queue<int>();
	    bfs_ds.Enqueue(0);
    	make_start();
    }

    public void click_dfs()
    {
    	gameMode = 1;
		instructionHeading.GetComponent<Text>().text = "Start selecting nodes in DFS manner";
		topHeading.GetComponent<Text>().text = "DFS Algorithm";
		dfs_ds = new Stack<int>();
		dfs_ds.Push(0);
    	make_start();
    }

    void make_start()
    {
    	background.GetComponent<SpriteRenderer>().sprite = backg;
    	background.transform.localScale = new Vector3 (1.4f, 1.4f, 1);
    	topHeading.transform.localScale = new Vector3 (0.6f, 0.6f, 1);
    	topHeading.transform.position = new Vector3 (topHeading.transform.position.x, topHeading.transform.position.y + 0.8f, 0);
    	didStart = true;
    	playerObj.SetActive(true);
    	pizzaObj.SetActive(true);
	    but_bfs.SetActive(false);
	    but_dfs.SetActive(false);
	    make_positions();
    }

    void make_gameOver()
    {
		gameOver = true;
    	playerObj.SetActive(false);
    	pizzaObj.SetActive(false);
    	topHeading.GetComponent<Text>().text = "All Pizzas delivered!\nAlgorithm completed successfully!";
    	instructionHeading.GetComponent<Text>().text = "";
    }

    void make_positions()
    {
    	int curLen = parent_of.Length;
	    node_pos = new float[curLen, 2];
	    int[] cur_capacity = new int[curLen];
	    int[] cur_level = new int[curLen];
	    for(int i = 0;i < curLen;i ++)
	    {
	    	cur_capacity[i] = get_capacity(i);
	    	cur_level[i] = get_level(i);
	    }
	    for(int i = 0;i < curLen;i ++)
	    {
	    	if(cur_level[i] == 1)
	    	{
	    		node_pos[i, 0] = 0f;
			    node_pos[i, 1] =3f;
	    	}
	    	else if(cur_level[i] == 2)
	    	{
    			node_pos[i, 0] = node_pos[parent_of[i], 0] - ((2 * cur_capacity[parent_of[i]] - 3) * 4f);
			    node_pos[i, 1] = node_pos[parent_of[i], 1] - 1.5f;
	    		cur_capacity[parent_of[i]] --;
	    	}
	    	else if(cur_level[i] == 3)
	    	{
    			node_pos[i, 0] = node_pos[parent_of[i], 0] - ((2 * cur_capacity[parent_of[i]] - 3) * 2f);
			    node_pos[i, 1] = node_pos[parent_of[i], 1] - 2f;
	    		cur_capacity[parent_of[i]] --;
	    	}
	    	else
	    	{
	    		node_pos[i, 0] = node_pos[parent_of[i], 0] - ((2 * cur_capacity[parent_of[i]] - 3) * 1f);
			    node_pos[i, 1] = node_pos[parent_of[i], 1] - 2f;
	    		cur_capacity[parent_of[i]] --;
	    	}
	    }
	    Debug.Log("Posititons made");
    }

    public bool click_node(int myID, Vector3 clickPos)
    {
    	// Debug.Log("ID " + myID + " has been clicked at " + clickPos);
    	clickPos = new Vector3(clickPos.x + 1f, clickPos.y - 0.5f, 0);
    	if(gameMode == 0)
    	{
	    	int curNode = bfs_ds.Peek();
	    	if(curNode != myID)
	    	{
	    		instructionHeading.GetComponent<Text>().text = "Wrong Node selected";
	    		return false;
	    	}
	    	curNode = bfs_ds.Dequeue();
	    	int curLen = parent_of.Length;
	    	for(int i = 0;i < curLen;i ++)
		    {
		    	if(parent_of[i] == myID)
		    	{
		    		bfs_ds.Enqueue(i);
		    	}
		    }
	    	playerObj.transform.position = clickPos;
	    	instructionHeading.GetComponent<Text>().text = "Nice, select the next node";
	    	if(bfs_ds.Count == 0)
	    	{
	    		make_gameOver();
	    	}
	    	return true;
	    }
	    else
	    {
	    	int curNode = dfs_ds.Peek();
	    	if(curNode != myID)
	    	{
	    		instructionHeading.GetComponent<Text>().text = "Wrong Node selected";
	    		return false;
	    	}
	    	curNode = dfs_ds.Pop();
	    	int curLen = parent_of.Length;
	    	for(int i = curLen - 1;i >= 0;i --)
		    {
		    	if(parent_of[i] == myID)
		    	{
		    		dfs_ds.Push(i);
		    	}
		    }
	    	playerObj.transform.position = clickPos;
	    	instructionHeading.GetComponent<Text>().text = "Nice, select the next node";
	    	if(dfs_ds.Count == 0)
	    	{
	    		make_gameOver();
	    	}
	    	return true;
	    }
    }

    public float get_position_x(int myID)
    {
    	return node_pos[myID, 0];
    }

    public float get_position_y(int myID)
    {
    	return node_pos[myID, 1];
    }

    public bool get_didStart()
    {
    	return didStart;
    }

    public bool get_clickStart()
    {
    	return clickStart;
    }

    public bool get_gameOver()
    {
    	return gameOver;
    }
}

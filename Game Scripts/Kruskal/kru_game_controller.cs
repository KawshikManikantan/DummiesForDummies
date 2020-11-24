using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class kru_game_controller : MonoBehaviour
{
	struct edge_box
	{
		public GameObject GO;
		public int val;
		public int ln;
		public int rn;
        public GameObject LineGO;
	}

	public GameObject rect_edge;
	public GameObject edval_inp_txt;
	public GameObject lnode_txt;
	public GameObject rnode_txt;
	public GameObject number_tab;
    public GameObject node_empty;
    public GameObject line_edge;
    public GameObject obj_inst;
	public Canvas Canvas_node;
	public Canvas Canvas_inst;
	public GameObject colourTab;
    [Range(1f, 2f)] [SerializeField] public float scale_val = 1.2f;
	public Color red;
    public Color green;
	edge_box[] rects;
	int count;
	int lr_node;//-1 is left, 1 is right
	bool didStart;
	bool gameOver;
    int curpos;
    int[] dsu_size;
    int[] dsu_rep;
    Text inst;

    void Start()
    {
    	count = 0;
    	lr_node = 0;
        curpos = 0;
    	didStart = false;
    	gameOver = false;
    	rects = new edge_box[0];
    	number_tab.SetActive(false);
        node_empty.SetActive(false);
        colourTab.SetActive(false);
    	Canvas_node.GetComponent<Canvas>().enabled = false;
        inst = obj_inst.GetComponent<Text>();
        inst.text = "";
        dsu_rep = new int[7];
        dsu_size = new int[7];
        for(int i = 0;i <= 6;i ++)
        {
            dsu_rep[i] = i;
            dsu_size[i] = 1;
        }
        // scale_val = 1.4f;
    }

    public void change_sel_node(int id)
    {
    	if(lr_node == -1)
    	{
    		lnode_txt.GetComponent<Text>().text = id + "";
    		lnode_txt.GetComponent<Text>().fontSize = 100;
    	}
    	else
    	{
    		rnode_txt.GetComponent<Text>().text = id + "";
    		rnode_txt.GetComponent<Text>().fontSize = 100;
    	}
    	number_tab.SetActive(false);
    }

    public void click_left_node()
    {
    	number_tab.SetActive(true);
    	lr_node = -1;
    }

    public void click_right_node()
    {
    	number_tab.SetActive(true);
    	lr_node = 1;
    }

    public void click_inst()
    {
        Canvas_inst.GetComponent<Canvas>().enabled = false;
        Canvas_node.GetComponent<Canvas>().enabled = true;
        node_empty.SetActive(true);
    }

    public void click_add_node()
    {
    	number_tab.SetActive(false);
    	if(lnode_txt.GetComponent<Text>().text == "Left Node" || rnode_txt.GetComponent<Text>().text == "Right Node")
    	{
    		inst.text = "Select a node";
    		return;
    	}
    	string sedval = edval_inp_txt.GetComponent<Text>().text;
    	int ilnode, irnode;
    	int iedval;
    	if(!Int32.TryParse(sedval, out iedval))
    	{
    		inst.text = "Not a number";
    		return;
    	}
    	if(!(iedval >= -20 && iedval <= 20))
    	{
    		inst.text = "Range is -20 to +20";
    		return;
    	}
    	if(count == 15)
    	{
    		inst.text = "Max edges reached";
    		return;
    	}
    	ilnode = Int32.Parse(lnode_txt.GetComponent<Text>().text);
    	irnode = Int32.Parse(rnode_txt.GetComponent<Text>().text);
    	if(edge_present(ilnode, irnode) != -1)
    	{
    		inst.text = "Edge already present";
    		return;
    	}
    	if(ilnode == irnode)
    	{
    		inst.text = "Self edges are not valid";
    		return;
    	}
    	GameObject cur_rect_edge = Instantiate(rect_edge, get_rect_pos(++ count), Quaternion.identity);
    	cur_rect_edge.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = sedval;
    	cur_rect_edge.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "(" + lnode_txt.GetComponent<Text>().text + "," + rnode_txt.GetComponent<Text>().text + ")";
        GameObject cur_line_edge = Instantiate(line_edge, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer lineRenderer = cur_line_edge.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = get_node_pos(ilnode);
        points[1] = get_node_pos(irnode);
        lineRenderer.SetPositions(points);
    	add_rects_GO(cur_rect_edge, iedval, ilnode, irnode, cur_line_edge);
        inst.text = "";
    }

    Vector3 get_node_pos(int id)
    {
        if(id == 1)
        {
            return new Vector3(-2f, 3f, 0f);
        }
        else if(id == 2)
        {
            return new Vector3(-4.6f, 1.5f, 0f);
        }
        else if(id == 3)
        {
            return new Vector3(-4.6f, -1.5f, 0f);
        }
        else if(id == 4)
        {
            return new Vector3(-2f, -3f, 0f);
        }
        else if(id == 5)
        {
            return new Vector3(0.6f, -1.5f, 0f);
        }
        else
        {
            return new Vector3(0.6f, 1.5f, 0f);
        }
    }

    int edge_present(int ilnode, int irnode)
    {
    	int curLen = rects.Length;
    	for(int i = 0;i < curLen;i ++)
    	{
    		int clnode = rects[i].ln;
    		int crnode = rects[i].rn;
    		if((ilnode == clnode && irnode == crnode) || (ilnode == crnode && irnode == clnode))
    		{
    			return i;
    		}
    	}
    	return -1;
    }

    void add_rects_GO(GameObject cur_rect_edge, int iedval, int ilnode, int irnode, GameObject cur_line_edge)
    {
    	int curLen = rects.Length;
    	edge_box[] temp = new edge_box[curLen + 1];
    	for(int i = 0;i < curLen;i ++)
    	{
    		temp[i] = rects[i];
    	}
    	temp[curLen].GO = cur_rect_edge;
    	temp[curLen].val = iedval;
    	temp[curLen].ln = ilnode;
    	temp[curLen].rn = irnode;
        temp[curLen].LineGO = cur_line_edge;
    	rects = temp;
    }

    public void click_start()
    {
        if(count == 0)
        {
            inst.text = "Add an edge";
            return;
        }
        didStart = true;
        number_tab.SetActive(false);
        colourTab.SetActive(true);
        Canvas_node.GetComponent<Canvas>().enabled = false;
        sort_rects();
        remove_extra_nodes();
    }

    void sort_rects()
    {
    	int curLen = rects.Length;
    	edge_box minobj;
    	int minpos;
    	for(int i = 0;i < curLen;i ++)
    	{
    		minobj = rects[i];
    		minpos = i;
    		for(int j = i + 1;j < curLen;j ++)
    		{
    			if(rects[j].val < minobj.val)
    			{
    				minobj = rects[j];
    				minpos = j;
    			}
    		}
    		rects[minpos] = rects[i];
    		rects[i] = minobj;
    	}
    	for(int i = 0;i < curLen;i ++)
    	{
    		rects[i].GO.transform.position = get_rect_pos(i + 1);
            rects[i].LineGO.SetActive(false);
    	}
    }

    void remove_extra_nodes()
    {
    	int curLen = rects.Length;
    	int[] vis = new int[6];
    	for(int i = 0;i < curLen;i ++)
    	{
    		vis[rects[i].ln - 1] ++;
    		vis[rects[i].rn - 1] ++;
    	}
    	for(int i = 0;i < 6;i ++)
    	{
    		if(vis[i] == 0)
    		{
    			node_empty.transform.GetChild(i).gameObject.SetActive(false);
    		}
    	}
    }

    Vector3 get_rect_pos(int id)
    {
    	float dp = 1.39f;
    	float spawnx = 3.83f + dp * ((id - 1) % 4);
    	float spawny = 4.16f - dp * ((id - 1) / 4);
    	return new Vector3(spawnx, spawny, 1);
    }

    public void set_color(int choice)
    {
    	if(!(didStart && !gameOver))
    	{
    		return;
    	}
    	inst.text = "";
        if(!forms_cycle(rects[curpos].ln, rects[curpos].rn))
        {
            rects[curpos].LineGO.SetActive(true);
            rects[curpos].GO.GetComponent<SpriteRenderer>().color = green;
            if(choice == 1)
            {
            	inst.text = "Correct choice";
            }
            else
            {
            	inst.text = "Wrong Choice";
            }
        }
        else
        {
        	rects[curpos].GO.GetComponent<SpriteRenderer>().color = red;
        	if(choice == 0)
            {
            	inst.text = "Correct choice";
            }
            else
            {
            	inst.text = "Wrong Choice";
            }
        }
        rects[curpos].GO.transform.localScale = new Vector3(0.0917102098f, 0.164126828f, 1);
        curpos ++;
        if(curpos == rects.Length)
        {
            make_game_over();
        }
    }

    void make_game_over()
    {
    	gameOver = true;
        inst.text = "Game Over! Found MST";
    }

    int findRep(int id)
    {
        int temp = id;
        while(dsu_rep[temp] != temp)
            temp = dsu_rep[temp];
        return temp;
    }

    void union_nodes(int x, int y)
    {
        if(dsu_size[x] < dsu_size[y])
        {
            dsu_size[y] += dsu_size[x];
            dsu_rep[x] = y;
        }
        else
        {
            dsu_size[x] += dsu_size[y];
            dsu_rep[y] = x;
        }
    }

    bool forms_cycle(int lnode, int rnode)
    {
        int x = findRep(lnode);
        int y = findRep(rnode);
        if(x != y)
        {
            union_nodes(x, y);
            return false;
        }
        return true;
    }

    void Update()
    {
    	if(curpos < rects.Length && didStart && !gameOver)
    	{
	    	rects[curpos].GO.transform.localScale = new Vector3(0.0917102098f, 0.164126828f, 1) * scale_val;
	    }
    }
}

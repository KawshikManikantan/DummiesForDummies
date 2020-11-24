using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bfs_dfs_node_controller : MonoBehaviour
{
	public Sprite[] house;
	public Color newColor;
	int ranv;// random var to select house
	SpriteRenderer spriteRen;
	bool didStart, gameOver, isSelect, clickStart;
	bfs_dfs_game_controller controlscript;
	int parentID, myID;
    GameObject childmyID, childparID;

    void Start()
    {
    	ranv = UnityEngine.Random.Range(0, 3);
    	spriteRen = gameObject.GetComponent<SpriteRenderer>();
        didStart = false;
        gameOver = false;
        isSelect = false;
        clickStart = false;
        GameObject controlobj = GameObject.Find("Control Empty");
        controlscript = controlobj.GetComponent<bfs_dfs_game_controller>();
        childmyID = gameObject.transform.GetChild(0).gameObject;
    	childparID = gameObject.transform.GetChild(1).gameObject;
    	myID = Int32.Parse(childmyID.GetComponent<TextMesh>().text);
    	parentID = Int32.Parse(childparID.GetComponent<TextMesh>().text);
    	if(ranv == 0)
    	{
	    	spriteRen.sprite = house[0];
	    }
	    else if(ranv == 1)
    	{
	    	spriteRen.sprite = house[1];
	    }
	    else if(ranv == 2)
    	{
	    	spriteRen.sprite = house[2];
	    }
    }

    void Update()
    {
        if(!clickStart && controlscript.get_clickStart())
        {
            clickStart = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            childmyID.GetComponent<Renderer>().enabled = false;
            childparID.GetComponent<Renderer>().enabled = false;
        }
        if(!didStart && controlscript.get_didStart())
        {
	        didStart = true;
            gameObject.GetComponent<Renderer>().enabled = true;
            childmyID.GetComponent<Renderer>().enabled = true;
            childmyID.GetComponent<TextMesh>().color = newColor;
        	transform.position = new Vector3(controlscript.get_position_x(myID), controlscript.get_position_y(myID), 0);
        	transform.localScale = new Vector3 (0.2f,0.2f, 1);
        }
        if(didStart && controlscript.get_gameOver())
        {
            gameOver = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            childmyID.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnMouseDown()
    {
    	if(Input.GetMouseButtonDown(0) && didStart && !gameOver && !isSelect)
    	{
    		isSelect = controlscript.click_node(myID, transform.position);
            if(isSelect)
            {
                transform.localScale = new Vector3 (0.2f,0.2f, 1);
            }
    	}
    }

    void OnMouseEnter()
    {
        if(!isSelect && didStart && !gameOver)
        {
            transform.localScale = new Vector3 (0.25f,0.25f, 1);
        }
    }

    void OnMouseExit()
    {
    	if(!isSelect && didStart && !gameOver)
    	{
	        transform.localScale = new Vector3 (0.2f,0.2f, 1);
	    }
    }
}

using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grey_box_controller : MonoBehaviour
{
	int myVal;
    MeshRenderer meshren;
    GameObject kruobj;
    kru_game_controller krucon;
	
    void Start()
    {
        kruobj = GameObject.Find("Control Empty");
        krucon = kruobj.GetComponent<kru_game_controller>();
        myVal = Int32.Parse(transform.GetChild(0).gameObject.GetComponent<TextMesh>().text);
        meshren = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        meshren.sortingLayerName = "Bar Layer";
        transform.localScale = new Vector3 (0.25f,0.25f, 1);
    }

    void OnEnable()
    {
        transform.localScale = new Vector3 (0.25f,0.25f, 1);
    }

    void OnMouseEnter()
    {
    	transform.localScale = new Vector3 (0.3f,0.3f, 1);
    }

    void OnMouseExit()
    {
    	transform.localScale = new Vector3 (0.25f,0.25f, 1);
    }

    void OnMouseDown()
    {
    	if(Input.GetMouseButtonDown(0))
    	{
            krucon.change_sel_node(myVal);
    		// Debug.Log(myVal);
    	}
    }
}

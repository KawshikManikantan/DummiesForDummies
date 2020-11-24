using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_tab_controller : MonoBehaviour
{
    GameObject kruobj;
    kru_game_controller krucon;

	void Start()
	{
        kruobj = GameObject.Find("Control Empty");
        krucon = kruobj.GetComponent<kru_game_controller>();
	}

    void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos -= transform.position;
           	int choice = -1;
            // Debug.Log(mousePos);
            if(mousePos.y >= -0.4f)
            {
            	// Debug.Log("GREEN");
            	choice = 1;
            }
            else
            {
            	// Debug.Log("RED");
            	choice = 0;
            }
            krucon.set_color(choice);
        }
    }
}

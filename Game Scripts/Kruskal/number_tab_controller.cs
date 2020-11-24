using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number_tab_controller : MonoBehaviour
{
	public GameObject grey_box;
	int count;
    
    void Start()
    {
    	count = 0;
    	for(int i = 1;i <= 6;i ++)
    	{
	        GameObject cur_grey_box = Instantiate(grey_box, new Vector3(3, 3, 1), Quaternion.identity);
	        cur_grey_box.transform.parent = gameObject.transform;
	        cur_grey_box.transform.position = get_grey_pos(++ count);
	        cur_grey_box.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = i + "";
	    }
    }

    Vector3 get_grey_pos(int id)
    {
    	float dpx = 1.3f;
    	float dpy = 1.6f;
    	float spawnx = 5.1f + dpx * ((id - 1) % 3);
    	float spawny = 2.25f - dpy * ((id - 1) / 3);
    	return new Vector3(spawnx, spawny, 1);
    }
}

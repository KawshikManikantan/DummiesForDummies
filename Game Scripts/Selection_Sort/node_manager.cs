using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_manager : MonoBehaviour
{
	bool isSelect;
	GameObject mc, child_text;
	maincon mc_script;
	TextMesh texte;

	void Start()
	{
		isSelect = false;
		mc = GameObject.Find("Main Empty");
		mc_script = mc.gameObject.GetComponent<maincon>();
		child_text = transform.GetChild(0).gameObject;
	    texte = child_text.GetComponent<TextMesh>();
	}

	void Update()
	{
		if(!isSelect)
		{
			float num_pos = mc_script.make_select();
			if(num_pos != -1 && ((this.gameObject.transform.localPosition.x - 6.5f)/1.5f) - num_pos <= 0.01)
			{
				this.gameObject.transform.localPosition = new Vector3 (mc_script.get_select_pos(), 9, 0);
			}
		}
	}

	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0) && !isSelect)
		{
			mc_script.click_node(texte.text, this.gameObject.transform.localPosition.x);
			int num_pos = mc_script.make_select();
			if(num_pos != -1)
			{
				isSelect = true;
				this.gameObject.transform.localPosition = new Vector3 (6.5f + num_pos * 1.5f, 9, 0);
				this.gameObject.transform.localScale = new Vector3 (0.1f,0.1f, 1);
			}
		}
	}

	void OnMouseEnter()
	{
		if(!isSelect)
		{
			transform.localScale = new Vector3 (0.12f,0.12f, 1);
		}
	}

	void OnMouseExit()
	{
		this.gameObject.transform.localScale = new Vector3 (0.1f,0.1f, 1);
	}
}

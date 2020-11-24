using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphmov : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3 (mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    // OnMouseDown is called when the user has pressed the mouse button while over the Collider
    private void OnMouseDown() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            isBeingHeld = true;
        }
    }

    // OnMouseUp is called when the user has released the mouse button
    public void OnMouseUp()
    {
        isBeingHeld = false;
    } 
}

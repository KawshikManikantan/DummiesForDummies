using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rect_edge_controller : MonoBehaviour
{
	MeshRenderer meshren;

    void Start()
    {
        meshren = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        meshren.sortingLayerName = "Node Layer";
        meshren = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
        meshren.sortingLayerName = "Node Layer";
    }
}

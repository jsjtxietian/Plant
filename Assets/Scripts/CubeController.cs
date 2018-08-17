using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private int x;
    private int y;
    private Helper Helper ;
    private AddToolController AddToolController;

	void Start ()
	{
	    x = Helper.GetChildOrder(gameObject.transform.parent.gameObject);
	    y = Helper.GetChildOrder(gameObject.transform.parent.parent.gameObject);
        AddToolController = GameObject.Find("Controller").GetComponent<AddToolController>();
    }

    public void OnMouseEnter()
    {
        AddToolController.SetNewOnePos(x,y);
    }

    public void OnMouseDown()
    {
        AddToolController.AddHandToIns(x,y);
    }

    public void OnMouseExit()
    {
        AddToolController.SetNewOnePos(-1, -1);
    }

}

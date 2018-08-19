using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private int x;
    private int y;
    private Helper Helper ;
    private AddToolController AddToolController;

    void Awake()
    {
        x = Helper.GetChildOrder(gameObject.transform.parent.gameObject);
        y = Helper.GetChildOrder(gameObject.transform.parent.parent.gameObject);
        GameObject.Find("Controller").GetComponent<WorldMapController>().Cubes[x, y] = gameObject;
    }

    void Start ()
	{
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

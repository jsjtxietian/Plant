using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSpriteController : MonoBehaviour
{
    private int x , y;

    void Awake()
    {
        GetThisPos();
        Instructions.CommandObjects[x, y] = gameObject;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GetThisPos()
    {
        //x -- the xth hand
        x = Helper.GetChildOrder(gameObject.transform.parent.parent.gameObject);

        //y -- the yth command 
        int bias = Helper.GetChildOrder(gameObject.transform.parent.gameObject) == 2 ? 6 : 0;
        int origin = Helper.GetChildOrder(gameObject);
        y = bias + origin;
    }

    public Coordinate GetCoordinate()
    {
        return new Coordinate(x,y);
    }
}

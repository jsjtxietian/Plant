using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSpriteController : MonoBehaviour
{
    private int order ;
    private GameObject Controller;

    void Awake()
    {
        Controller = GameObject.Find("Controller");
        order = FindHandOrder();
        Instructions.HandObjects[order] = gameObject;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int FindHandOrder()
    {
        Transform grand = gameObject.transform.parent.parent;
        for (int i = 0; i < grand.childCount; i++)
        {
            if (grand.GetChild(i).GetChild(0).gameObject.Equals(gameObject))
                return i;
        }
        return -1;
    }

   
}

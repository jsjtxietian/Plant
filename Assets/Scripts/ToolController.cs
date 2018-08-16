using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolController : MonoBehaviour,IPointerClickHandler {

    public GameObject Controller;

	// Use this for initialization
	void Start ()
	{
	    Controller = GameObject.Find("Controller");
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.GetComponent<Instructions>().AddHand(Helper.GetHandTypeFromString(gameObject.GetComponent<Image>().sprite.name));
    }
}

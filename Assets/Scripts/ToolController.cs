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

    public void OnPointerClick(PointerEventData eventData)
    {
        Hand type = Helper.GetHandTypeFromString(gameObject.GetComponent<Image>().sprite.name);
        if (Controller.GetComponent<Instructions>().CheckHandLessThan2(type))
        {
            Controller.GetComponent<AddToolController>().AddHandObject(type);
        }
    }
}

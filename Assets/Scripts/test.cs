using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    //public void OnMouseEnter()
    //{
    //    Debug.Log("Enter");
    //}

    //public void OnMouseDown()
    //{
    //    Debug.Log("click");
    //}

    //public void OnMouseExit()
    //{
    //    Debug.Log("Leave");
    //}

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            Debug.Log(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log(hit.point);
            //}
        }
    }
}
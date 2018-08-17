using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToolController : MonoBehaviour {

    private Helper Helper;
    private Instructions Instructions;


    private GameObject newOne = null;
    private Hand currentType;

    void Start()
    {
        Helper = gameObject.GetComponent<Helper>();
        Instructions = gameObject.GetComponent<Instructions>();
    }

    public void AddHandObject(Hand type)
    {
        Destroy(newOne);
        newOne = null;
        currentType = type;

        switch (type)
        {
            case Hand.Big:
                newOne = (GameObject)Resources.Load("Prefabs/BigHand");
                break;
            case Hand.Small:
                newOne = (GameObject)Resources.Load("Prefabs/SmallHand");
                break;
            case Hand.Human:
                newOne = (GameObject)Resources.Load("Prefabs/HumanHand");
                break;
        }

        newOne = Instantiate(newOne);
        newOne.transform.position = new Vector3(1000, 1000, 1000);
    }

    public void SetNewOnePos(int x, int y)
    {
        if (newOne == null)
            return;

        if(x == -1)
        {
            newOne.transform.position = new Vector3(1000, 1000, 1000);
        }
        else
        {
            newOne.transform.position = Helper.GetPos(x, y);
            Debug.Log(newOne.transform.position);
        }
    }

    public void AddHandToIns(int x, int y)
    {
        Instructions.AddHand(currentType);
        newOne = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour {

    public Coordinate CurrentPos = new Coordinate();

    public void SetPos(int _x , int _y)
    {
        CurrentPos.x = _x;
        CurrentPos.y = _y;
    }

}

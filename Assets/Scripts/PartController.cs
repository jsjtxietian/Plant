using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour {

    public Coordinate CurrentPos = new Coordinate();
    public Coordinate InitPos = new Coordinate();
    public ComponentType type;

    public void SetPos(int _x , int _y)
    {
        CurrentPos.x = _x;
        CurrentPos.y = _y;
    }

    public void SetInitPos(int _x, int _y)
    {
        InitPos.x = _x;
        InitPos.y = _y;
        SetPos(_x,_y);
    }

    public void SetPos(Coordinate pos)
    {
        CurrentPos.x = pos.x;
        CurrentPos.y = pos.y;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHandController : MonoBehaviour,HandAction {

    public Coordinate initPos;

    public bool CheckOutBound(Coordinate nextPos)
    {
        if (nextPos.x > 9 || nextPos.x < 0
            || nextPos.y > 9 || nextPos.y < 0)
            return false;
        if ((nextPos.x <= initPos.x + 1 && nextPos.x >= initPos.x)
            && (nextPos.y <= initPos.y + 1 && nextPos.y >= initPos.y))
            return true;
        else
        {
            return false;
        }
    }

    public Coordinate GetInitPos()
    {
        return initPos;
    }

    public bool CanPick(ComponentType type)
    {
        if (type == ComponentType.Fit || type == ComponentType.Normal)
            return true;
        else
        {
            return false;
        }
    }

    public void SetInitPos(Coordinate pos)
    {
        initPos = new Coordinate();
        initPos.x = pos.x;
        initPos.y = pos.y;
    }
}

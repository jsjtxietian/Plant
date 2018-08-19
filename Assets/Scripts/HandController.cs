using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Coordinate initPos;
    public Coordinate currentPos;

    public int bias;
    public Hand thisType;

    public GameObject Controller;
    private Helper Helper;

    void Start()
    {
        if (gameObject.name.Contains("Big"))
        {
            thisType = Hand.Big;
            bias = 3;
        }
        else if (gameObject.name.Contains("Small"))
        {
            thisType = Hand.Small;
            bias = 1;
        }
        else if (gameObject.name.Contains("Human"))
        {
            thisType = Hand.Human;
            bias = 2;
        }

        Controller = GameObject.Find("Controller");
        Helper = Controller.GetComponent<Helper>();
    }

    public bool CheckOutBound(Coordinate nextPos)
    {
        //out map
        if (nextPos.x > 9 || nextPos.x < 0
            || nextPos.y > 9 || nextPos.y < 0)
            return true;

        if (thisType == Hand.Big || thisType == Hand.Small)
        {
            if (nextPos.x > initPos.x + bias || nextPos.y > initPos.y + bias)
                return true;
        }
        else
        {
            if (nextPos.x < initPos.x - bias || nextPos.y < initPos.y - bias)
                return true;
        }
        return false;
    }

    public bool Move(Command type)
    {
        Coordinate nextPos;

        switch (type)
        {
            case Command.Up:
                nextPos = new Coordinate(currentPos.x, currentPos.y + bias);
                break;
            case Command.Down:
                nextPos = new Coordinate(currentPos.x, currentPos.y - bias);
                break;
            case Command.Left:
                nextPos = new Coordinate(currentPos.x - bias, currentPos.y);
                break;
            case Command.Right:
                nextPos = new Coordinate(currentPos.x + bias, currentPos.y);
                break;
            default:
                nextPos = new Coordinate(-1, -1);
                break;
        }

        if (CheckOutBound(nextPos))
        {
            return false;
        }
        else
        {
            transform.DOMove(Helper.GetHandPos(nextPos.x, nextPos.y), Config.RoundTime);
            return true;
        }
    }

    public Coordinate GetInitPos()
    {
        return initPos;
    }

    public bool CanPick(ComponentType type)
    {
        switch (thisType)
        {
            case Hand.Big:
                if (type == ComponentType.Complex || type == ComponentType.Normal || type == ComponentType.Heavy)
                    return true;
                else
                    return false;
            case Hand.Human:
                if (type == ComponentType.Complex || type == ComponentType.Normal)
                    return true;
                else
                    return false;
            case Hand.Small:
                if (type == ComponentType.Fit || type == ComponentType.Normal)
                    return true;
                else
                    return false;
            default:
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
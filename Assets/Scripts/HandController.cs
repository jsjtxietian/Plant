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
    private WorldMapController WorldMapController;
    private Instructions Instructions;
    public List<GameObject> PickedParts;

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
        WorldMapController = Controller.GetComponent<WorldMapController>();
        Instructions = Controller.GetComponent<Instructions>();
        PickedParts = new List<GameObject>();
    }

    public bool CheckOutBound(Coordinate nextPos)
    {
        //out map
        if (nextPos.x > 9 || nextPos.x < 0
            || nextPos.y > 9 || nextPos.y < 0)
            return true;

        if (thisType == Hand.Big || thisType == Hand.Small)
        {
            if (nextPos.x > initPos.x + bias || nextPos.y > initPos.y + bias 
                   || nextPos.x < initPos.x || nextPos.y < initPos.y)
                return true;
        }
        else
        {
            if (nextPos.x < initPos.x - bias || nextPos.y < initPos.y - bias
                || nextPos.x > initPos.x || nextPos.y > initPos.y)
                return true;
        }

        return false;
    }

    public void Move(Command type , Coordinate nextPos)
    {
        transform.DOMove(Helper.GetHandPos(nextPos.x, nextPos.y), Config.RoundTime);
        PickedParts.ForEach(x =>
        {
            x.transform.DOMove(Helper.GetPartPos(nextPos.x, nextPos.y), Config.RoundTime);
        });
        WorldMapController.AfterMove(currentPos,nextPos,thisType);
        currentPos = nextPos;
    }

    public void Excute(Command type)
    {
        Coordinate nextPos = GetNextPos(type);

        switch (type)
        {
            case Command.Pick:
                Pick(nextPos);
                break;
            case Command.Put:
                Put();
                break;
            case Command.Active:
            case Command.None:
                break;
            default://move
                Move(type , nextPos);
                break;
        }
    }

    private void Pick(Coordinate pos)
    {
        GameObject picked = WorldMapController.GetPartByPos(pos);
        PickedParts.Add(picked);
        WorldMapController.AfterPick(pos);
    }

    private void Put()
    {
        foreach (var pickedPart in PickedParts)
        {
            PartController p = pickedPart.GetComponent<PartController>();
            p.CurrentPos = currentPos;
            WorldMapController.AfterPut(p.CurrentPos, p.type);
        }

        PickedParts.Clear();
    }

    public void Reset()
    {
        gameObject.transform.position = Helper.GetHandPos(initPos.x, initPos.y);
        WorldMapController.WorldMap[initPos.x, initPos.y].hand = thisType;
        WorldMapController.WorldMap[currentPos.x, currentPos.y].hand = Hand.None;
        currentPos = initPos;

        PickedParts.Clear();
    }

    public Coordinate GetCurrentPos()
    {
        return currentPos;
    }

    public Coordinate GetNextPos(Command type)
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
            case Command.Pause:
            case Command.None:
            case Command.Pick:
            case Command.Put:
            case Command.Active:
                nextPos = new Coordinate(currentPos.x, currentPos.y);
                break;
            default:
                nextPos = new Coordinate(-1, -1);
                break;
        }

        return nextPos;
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
                if (type == ComponentType.Fit || type == ComponentType.Normal || type == ComponentType.Heavy)
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
            case Hand.None:
                return false;
            default:
                return false;
        }
    }

    public bool CanStepOn(Coordinate pos, WorldMapElement e)
    {
        if ((thisType == Hand.Big || thisType == Hand.Small) && e == WorldMapElement.Flexible)
            return false;
        return true;
    }

    public void SetInitPos(Coordinate pos)
    {
        initPos = new Coordinate();
        initPos.x = pos.x;
        initPos.y = pos.y;

        currentPos = new Coordinate(initPos.x, initPos.y);
    }

}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Linq;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class Instructions : MonoBehaviour
{
    public GameObject CommandsArea;
    private WorldMapController WorldMapController;

    public static Command[,] CommandList = new Command[6, 12];
    public static List<Hand> HandList = new List<Hand>();

    public static List<GameObject> HandObjects = new List<GameObject>();
    public static List<GameObject> GroundMasks = new List<GameObject>();

    public static GameObject[,] CommandSpriteObjects = new GameObject[6, 12];
    public static GameObject[] HandSpriteObjects = new GameObject[6];

    void Start()
    {
        WorldMapController = gameObject.GetComponent<WorldMapController>();

        UpdateHandGUI();
        UpdateCommandGUI();
    }

    public void AddCommand(int x, int y, Command newCommand)
    {
        CommandList[x, y] = newCommand;
        if (y < 11 && CommandList[x, y + 1] == Command.None)
        {
            CommandList[x, y + 1] = Command.Active;
        }
        UpdateCommandGUI();
    }

    public void AddHand(Coordinate pos, Hand newHand, GameObject newOne)
    {
        if (HandList.Count > 6 || !CheckHandLessThan2(newHand))
            return;

        WorldMapController.AddHandToMap(pos,newHand);

        HandList.Add(newHand);
        CommandList[HandList.Count - 1, 0] = Command.Active;

        //spilt ground mask & hand
        GameObject gm = newOne.transform.GetChild(0).gameObject;
        GroundMasks.Add(gm);
        gm.transform.parent = GameObject.Find("GroundMaskContainer").transform;

        HandObjects.Add(newOne);

        UpdateHandGUI();
        UpdateCommandGUI();
    }

    public void DeleteHand(int order)
    {
        Coordinate handPos = HandObjects[order].GetComponent<HandController>().currentPos;

        WorldMapController.WorldMap[handPos.x, handPos.y].hand = Hand.None;

        HandList.RemoveAt(order);

        Destroy(HandObjects[order]);
        HandObjects.RemoveAt(order);

        Destroy(GroundMasks[order]);
        GroundMasks.RemoveAt(order);

        for (int i = order; i < 5; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                CommandList[i, j] = CommandList[i + 1, j];
            }
        }

        for (int j = 0; j < 12; j++)
        {
            CommandList[5, j] = Command.None;
        }

        UpdateHandGUI();
        UpdateCommandGUI();
    }

    public void DeleteCommand(Coordinate tobeDeleted)
    {
        if (tobeDeleted.y == 11)
            CommandList[tobeDeleted.x, 11] = Command.Active;
        else
        {
            for (int j = tobeDeleted.y; j < 11; j++)
            {
                CommandList[tobeDeleted.x, j] = CommandList[tobeDeleted.x, j + 1];
            }
            CommandList[tobeDeleted.x, 11] = Command.None;
        }

        UpdateCommandGUI();
    }

    public void UpdateCommandGUI()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (CommandList[i, j] == Command.None)
                {
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = false;
                    CommandSpriteObjects[i, j].GetComponent<Image>().sprite =
                        Helper.CommandSpriteDictionary[Command.None];
                    Helper.SetTransparent(CommandSpriteObjects[i, j].GetComponent<Image>(), 0);
                }
                else if (CommandList[i, j] == Command.Active)
                {
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = false;
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = true;
                    CommandSpriteObjects[i, j].GetComponent<Image>().sprite =
                        Helper.CommandSpriteDictionary[Command.Active];
                }
                else
                {
                    CommandSpriteObjects[i, j].GetComponent<Image>().sprite =
                        Helper.CommandSpriteDictionary[CommandList[i, j]];
                }
            }
        }
    }

    public void UpdateHandGUI()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i < HandList.Count)
            {
                HandSpriteObjects[i].SetActive(true);
                HandSpriteObjects[i].GetComponent<Image>().sprite = Helper.HandSpriteDictionary[HandList[i]];
            }
            else
                HandSpriteObjects[i].SetActive(false);
        }
    }

    public int GetMaxLength()
    {
        int currentMax = 0;

        for (int i = 0; i < 6; i++)
        {
            int currentLength = 0;

            for (int j = 0; j < 12; j++)
            {
                if(CommandList[i,j] == Command.Active || CommandList[i,j] == Command.None)
                    break;
                currentLength++;
            }

            if (currentLength > currentMax)
                currentMax = currentLength;
        }

        return currentMax;
    }

    public List<Command> GetThisRoundCommand(int round)
    {
        List<Command> currentCommands = new List<Command>();

        for (int i = 0; i < HandList.Count; i++)
        {
            Command tempCommand = CommandList[i, round];
            currentCommands.Add(tempCommand);
        }

        return currentCommands;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintCommandList();
        }
    }

    public void ResetCommandArea()
    {
        HandList.Clear();

        for (int i = 0; i < 6; i++)
        for (int j = 0; j < 12; j++)
            CommandList[i, j] = Command.None;

        HandObjects.Destroy();

        UpdateHandGUI();
        UpdateCommandGUI();
    }

    public bool CheckHandLessThan2(Hand newHand)
    {
        int bigCount = 0, smallCount = 0, humanCount = 0;
        HandList.Add(newHand);

        HandList.ForEach(x =>
        {
            switch (x)
            {
                case Hand.Big:
                    bigCount++;
                    break;
                case Hand.Small:
                    smallCount++;
                    break;
                case Hand.Human:
                    humanCount++;
                    break;
            }
        });

        HandList.RemoveAt(HandList.Count - 1);

        return bigCount <= 2 && smallCount <= 2 && humanCount <= 2;
    }

    private void PrintCommandList()
    {
        StreamWriter s = new StreamWriter("CommandList.txt");
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                s.Write(CommandList[i, j].ToString() + ' ');
            }
            s.WriteLine(' ');
        }

        s.Dispose();
        s.Close();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Linq;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject CommandsArea;

    public static Command[,] CommandList = new Command[6, 12];
    public static List<Hand> HandList = new List<Hand>();

    public static List<GameObject> HandObjects = new List<GameObject>();

    public static GameObject[,] CommandSpriteObjects = new GameObject[6, 12];
    public static GameObject[] HandSpriteObjects = new GameObject[6];

    void Start()
    {
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

    public void AddHand(Hand newHand , GameObject newOne)
    {
        if (HandList.Count > 6 || !CheckHandLessThan2(newHand))
            return;

        HandList.Add(newHand);
        CommandList[HandList.Count - 1, 0] = Command.Active;

        HandObjects.Add(newOne);

        UpdateHandGUI();
        UpdateCommandGUI();
    }

    public void DeleteHand(int order)
    {
        HandList.RemoveAt(order);

        Destroy(HandObjects[order]);
        HandObjects.RemoveAt(order);

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
        //todo relate to hand
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (CommandList[i, j] == Command.None)
                {
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = false;
                    CommandSpriteObjects[i, j].GetComponent<Image>().sprite = Helper.CommandSpriteDictionary[Command.None];
                    Helper.SetTransparent(CommandSpriteObjects[i, j].GetComponent<Image>(), 0);
                }
                else if (CommandList[i, j] == Command.Active)
                {
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = false;
                    CommandSpriteObjects[i, j].GetComponent<DropMe>().enabled = true;
                    CommandSpriteObjects[i, j].GetComponent<Image>().sprite = Helper.CommandSpriteDictionary[Command.Active];
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
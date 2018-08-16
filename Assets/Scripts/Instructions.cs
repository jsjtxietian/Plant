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

    public static GameObject[,] CommandObjects = new GameObject[6, 12];
    public static GameObject[] HandObjects = new GameObject[6];

    void Start()
    {
        //UpdateCommandGUI();
    }

    public void AddCommand(int x, int y, Command newCommand)
    {
        CommandList[x, y] = newCommand;
        UpdateCommandGUI();
    }

    public void AddHand(Hand newHand)
    {
        if (HandList.Count > 6)
            return;
        HandList.Add(newHand);
        UpdateHandGUI();
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
                    CommandObjects[i, j].GetComponent<DropMe>().enabled = false;
                }
                else if (CommandList[i, j] == Command.Active)
                {
                    CommandObjects[i, j].GetComponent<DropMe>().enabled = true;
                }
                else
                {
                    CommandObjects[i, j].GetComponent<Image>().sprite =
                        Helper.CommandSpriteDictionary[CommandList[i, j]];
                }
            }
        }
    }

    public void UpdateHandGUI()
    {
        for (int i = 0; i < HandList.Count; i++)
        {
            HandObjects[i].GetComponent<Image>().sprite = Helper.HandSpriteDictionary[HandList[i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //PrintCommandList();
            UpdateCommandGUI();
        }
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
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
    public static Hand[] HandList = new Hand[6];

    public static GameObject[,] CommandObjects = new GameObject[6, 12];
    public static GameObject[] HandObjects = new GameObject[6];

    void Start()
    {
    }

    public void UpdateCommandGUI()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (CommandList[i, j] != Command.None)
                {
                    CommandObjects[i, j].GetComponent<Image>().sprite = Helper.CommandSpriteDictionary[CommandList[i, j]];
                }
            }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool IsGameOn = false;

    private WorldMapController WorldMapController;
    private Instructions Instructions;

    void Start()
    {
        WorldMapController = gameObject.GetComponent<WorldMapController>();
        Instructions = gameObject.GetComponent<Instructions>();
    }

    public void StartGame()
    {
        IsGameOn = true;
    }

    public void StopGame()
    {
        IsGameOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();

            temp.Move(Command.Left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();

            temp.Move(Command.Down);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();

            temp.Move(Command.Up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();

            temp.Move(Command.Right);
        }
    }
}
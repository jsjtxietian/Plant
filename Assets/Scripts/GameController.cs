using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool IsGameOn = false;

    public void StartGame()
    {
        IsGameOn = true;
    }

    public void StopGame()
    {
        IsGameOn = false;
    }

}

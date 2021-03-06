﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool IsGameOn = false;
    public int currentLevel;
    public GameObject SuccessUI;
    public GameObject WrongUI;
    public GameObject LittleTri;

    public AudioSource GameCrashAudio;
    public AudioSource DoingGoodAudio;

    private WorldMapController WorldMapController;
    private Instructions Instructions;
    private List<HandController> HandControllers;

    public GameObject StartButton;

    void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        //currentLevel = 0;
    }

    void Start()
    {
        WorldMapController = gameObject.GetComponent<WorldMapController>();
        Instructions = gameObject.GetComponent<Instructions>();
        HandControllers = new List<HandController>();

        if (GameObject.Find("BGM") != null)
        {
            Destroy(GameObject.Find("BGM"));
        }
    }

    public void StartGame()
    {
        ResetMap();
        IsGameOn = true;
        StartCoroutine(ExcuteIns());
    }

    public void StopGame()
    {
        IsGameOn = false;
        StartButton.GetComponent<StartButtonBeha>().StopGameDueToCrash();
        StopAllCoroutines();
    }

    IEnumerator ExcuteIns()
    {
        InitHandControllers();

        int length = Instructions.GetMaxLength();
        int currentIndex = 0;

        while (currentIndex < length)
        {
            //get the ins list
            List<Command> currentExcuteOnes = Instructions.GetThisRoundCommand(currentIndex);

            //check conflict
            CheckConflict(currentExcuteOnes);
            yield return new WaitForEndOfFrame();

            //excute
            for (int i = 0; i < currentExcuteOnes.Count; i++)
            {
                HandControllers[i].Excute(currentExcuteOnes[i]);
            }

            currentIndex++;

            //check whether wins
            if (CheckWin())
            {
                DoingGoodAudio.Play();
                SuccessUI.SetActive(true);
                yield return new WaitForSeconds(Config.RoundTime);

                int finishedLevel = PlayerPrefs.GetInt("FinishedLevel");
                if (currentLevel > finishedLevel)
                {
                    PlayerPrefs.SetInt("FinishedLevel", currentLevel);
                }

                if (currentLevel == 3)
                {
                    SceneManager.LoadScene("Congratulation");
                }
                else
                {
                    SceneManager.LoadScene("LevelSelection");
                }
            }

            yield return new WaitForSeconds(Config.RoundTime);
        }

        GameCrash();
    }

    private bool CheckWin()
    {
        if (WorldMapController.Parts.Count == 0)
            return true;
        else
            return false;
    }

    private void CheckConflict(List<Command> currentExcuteOnes)
    {
        //get all the next pos
        List<Coordinate> NextPoses = new List<Coordinate>();
        for (int i = 0; i < Instructions.HandObjects.Count; i++)
        {
            Coordinate currentNextPos = HandControllers[i].GetNextPos(currentExcuteOnes[i]);
            //1 . out bound
            if (HandControllers[i].CheckOutBound(currentNextPos))
            {
                Debug.Log("outbound");
                GameCrash();
                return;
            }

            //2 . big and small in flexible
            Grid nextGrid = WorldMapController.WorldMap[currentNextPos.x, currentNextPos.y];
            if (!HandControllers[i].CanStepOn(currentNextPos, nextGrid.map))
            {
                GameCrash();
                Debug.Log("big and small in flexible");
                return;
            }

            //4 . pick none
            //5 . pick correct ones
            if (currentExcuteOnes[i] == Command.Pick)
            {
                ComponentType type = WorldMapController.WorldMap[currentNextPos.x, currentNextPos.y].component;
                if (!HandControllers[i].CanPick(type))
                {
                    GameCrash();
                    Debug.Log("Pick Error");
                    return;
                }
            }

            //6.  none when put
            if (currentExcuteOnes[i] == Command.Put)
            {
                if (HandControllers[i].PickedParts.Count == 0)
                {
                    GameCrash();
                    Debug.Log("Put Wrong");
                    return;
                }
            }

            NextPoses.Add(currentNextPos);
        }

        //3 . Many in same grid
        if (CheckPosRepeat(NextPoses))
        {
            Debug.Log(" Many in same grid");
            GameCrash();
            return;
        }
    }

    private void GameCrash()
    {
        GameCrashAudio.Play();
        WrongUI.SetActive(true);
        StopGame();
    }

    private bool CheckPosRepeat(List<Coordinate> NextPoses)
    {
        for (int i = 0; i < NextPoses.Count; i++)
        {
            for (int j = i + 1; j < NextPoses.Count; j++)
            {
                if (NextPoses[i].Equal(NextPoses[j]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void InitHandControllers()
    {
        HandControllers.Clear();

        for (int i = 0; i < Instructions.HandObjects.Count; i++)
        {
            HandControllers.Add(Instructions.HandObjects[i].GetComponent<HandController>());
        }
    }

    private void ResetMap()
    {
        WorldMapController.ResetMap();
        Instructions.ResetMap();
    }

    public void ShowTri(int order)
    {
        Coordinate triCor = Instructions.HandObjects[order].GetComponent<HandController>().currentPos;

        StopCoroutine("DisappearTri");
        LittleTri.transform.position = gameObject.GetComponent<Helper>().GetTriPos(triCor.x,triCor.y);
        LittleTri.SetActive(true);
        StartCoroutine("DisappearTri");
    }

    IEnumerator DisappearTri()
    {
        yield return new WaitForSeconds(Config.RoundTime);
        LittleTri.SetActive(false);
    }
}
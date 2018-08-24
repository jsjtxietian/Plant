using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool IsGameOn = false;
    public int currentLevel;
    public GameObject SuccessUI;
    public GameObject WrongUI;

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
                GameCrash();
                Debug.Log("outbound");
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

            NextPoses.Add(currentNextPos);
        }

        //3 . Many in same grid
        if (CheckPosRepeat(NextPoses))
        {
            GameCrash();
            return;
        }
    }

    private void GameCrash()
    {
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Down);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Up);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Pick);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Excute(Command.Put);
        }
    }
}
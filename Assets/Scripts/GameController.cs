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

    void Start()
    {
        WorldMapController = gameObject.GetComponent<WorldMapController>();
        Instructions = gameObject.GetComponent<Instructions>();

        HandControllers = new List<HandController>();

        //todo read from playerprefabs
        currentLevel = 3;
    }

    public void StartGame()
    {
        IsGameOn = true;

        StartCoroutine(ExcuteIns());
    }

    public void StopGame()
    {
        IsGameOn = false;
        StopAllCoroutines();
    }

    IEnumerator ExcuteIns()
    {
        InitHandControllers();

        int length = Instructions.GetMaxLength();
        int currentIndex = 0;

        while (currentIndex < length)
        {
            //check whether wins
            if (CheckWin())
            {
                SuccessUI.SetActive(true);
                yield return new WaitForSeconds(Config.RoundTime);
                PlayerPrefs.SetInt("FinishedLevel", currentLevel);
                SceneManager.LoadScene("LevelSelection");
            }

            //get the ins list
            List<Command> currentExcuteOnes = Instructions.GetThisRoundCommand(currentIndex);

            //check conflict
            CheckConflict(currentExcuteOnes);
            yield return new WaitForEndOfFrame();

            //excute

            yield return new WaitForSeconds(Config.RoundTime);
            currentIndex++;
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
                return;
            }

            //2 . big and small in flexible
            Grid nextGrid = WorldMapController.WorldMap[currentNextPos.x, currentNextPos.y];
            if (HandControllers[i].CanStepOn(currentNextPos, nextGrid.map))
            {
                GameCrash();
                return;
            }

            NextPoses.Add(currentNextPos);
        }

        //3 . Many in same grid
        if (CheckPosRepeat(NextPoses))
        {
            GameCrash();
            return;
        }

        //todo check 
        //4 . pick none
        //5 . pick correct ones
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Move(Command.Left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Move(Command.Down);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Move(Command.Up);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            HandController temp = Instructions.HandObjects[0].GetComponent<HandController>();
            temp.Move(Command.Right);
        }
    }
}
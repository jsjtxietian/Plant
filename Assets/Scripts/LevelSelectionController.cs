using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public GameObject Levels;

    public GameObject GetStar(int i)
    {
        return GetButton(i).transform.GetChild(0).gameObject;
    }

    public void SetStar(int i)
    {
        GetStar(i-1).SetActive(true);
    }

    public GameObject GetButton(int i)
    {
        return Levels.transform.GetChild(i).gameObject;
    }

    public void AddEvents(int i)
    {
        Button button = GetButton(i-1).GetComponent<Button>();
        ClickEvents c = gameObject.GetComponent<ClickEvents>();

        switch (i)
        {
            case 1:
                button.onClick.AddListener(c.GotoLevel1);
                break;
            case 2:
                button.onClick.AddListener(c.GotoLevel2);
                break;
            case 3:
                button.onClick.AddListener(c.GotoLevel3);
                break;
        }

        Helper.SetTransparent(GetButton(i-1).GetComponent<Image>(),1f);
    }

    void Start ()
    {
        int finishedLevel = PlayerPrefs.GetInt("FinishedLevel");
        switch (finishedLevel)
        {
            case 0:
                AddEvents(1);
                break;
            case 1:
                AddEvents(1);
                SetStar(1);
                AddEvents(2);
                break;
            case 2:
                AddEvents(1);
                SetStar(1);

                AddEvents(2);
                SetStar(1);

                AddEvents(3);
                break;
            case 3:
                AddEvents(1);
                SetStar(1);

                AddEvents(2);
                SetStar(1);

                AddEvents(3);
                SetStar(1);

                break;
        }
    }
}

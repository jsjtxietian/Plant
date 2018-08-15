using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickEvents : MonoBehaviour
{
    #region P01

    public void P01ToP02()
    {
        SceneManager.LoadScene("P02");
    }

    #endregion

    #region P02

    public GameObject P2_UI1;
    public GameObject P2_UI2;
    public GameObject P2_UI3;
    public GameObject P2_UI_PlayMovie;
    public GameObject P2_UI_Next;
    public GameObject Video;


    public void P02_1_2()
    {
        P2_UI1.SetActive(false);
        P2_UI2.SetActive(true);
    }

    public void P02_2_3()
    {
        P2_UI2.SetActive(false);
        P2_UI3.SetActive(true);
    }

    public void P02_3_4()
    {
        P2_UI3.SetActive(false);
        P2_UI_PlayMovie.SetActive(true);
    }

    public void PlayMovie() //P02 teaching movie
    {
        P2_UI_PlayMovie.SetActive(false);
        Video.SetActive(true);
    }

    public void Replay() // P02 replay teaching movie
    {
        P2_UI_Next.SetActive(false);
        Video.SetActive(true);
    }

    #endregion

    #region Level

    public GameObject HintUI;

    public void ShowHint()
    {
        HintUI.SetActive(true);
    }

    public void HideHint()
    {
        HintUI.SetActive(false);
        Debug.Log("fuck");
    }

    #endregion

    public void ToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
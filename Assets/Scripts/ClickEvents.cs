using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickEvents : MonoBehaviour
{
    #region P01

    public AudioSource P01ClickAudioSource;
    public void P01ToP02()
    {
        P01ClickAudioSource.Play();
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
    public AudioSource Gotit;

    public void P02_1_2()
    {
        Gotit.Play();
        P2_UI1.SetActive(false);
        P2_UI2.SetActive(true);
    }

    public void P02_2_3()
    {
        Gotit.Play();

        P2_UI2.SetActive(false);
        P2_UI3.SetActive(true);
    }

    public void P02_3_4()
    {
        Gotit.Play();

        P2_UI3.SetActive(false);
        P2_UI_PlayMovie.SetActive(true);
    }

    public void PlayMovie() //P02 teaching movie
    {
        P01ClickAudioSource.Play();
        P2_UI_PlayMovie.SetActive(false);
        Video.SetActive(true);
    }

    public void Replay() // P02 replay teaching movie
    {
        P01ClickAudioSource.Play();
        P2_UI_Next.SetActive(false);
        Video.SetActive(true);
    }

    #endregion

    #region Level

    public GameObject HintUI;
    public GameObject Controller;
    public GameObject WrongUI;

    public void ShowHint()
    {
        P01ClickAudioSource.Play();
        HintUI.SetActive(true);
    }

    public void HideHint()
    {
        P01ClickAudioSource.Play();
        HintUI.SetActive(false);
    }

    public void ResetCommandArea()
    {
        P01ClickAudioSource.Play();
        gameObject.GetComponent<Instructions>().ResetCommandArea();
    }

    public void GoToInstruct()
    {
        P01ClickAudioSource.Play();
        WrongUI.SetActive(false);
    }

    #endregion

    #region LevelSelection

    public AudioSource Select;

    public void GotoLevel1()
    {
        Select.Play();
        PlayerPrefs.SetInt("CurrentLevel",1);
        SceneManager.LoadScene("Level");
    }

    public void GotoLevel2()
    {
        Select.Play();
        PlayerPrefs.SetInt("CurrentLevel", 2);
        SceneManager.LoadScene("Level");
    }

    public void GotoLevel3()
    {
        Select.Play();
        PlayerPrefs.SetInt("CurrentLevel", 3);
        SceneManager.LoadScene("Level");
    }

    #endregion LevelSelection

    public void ToLevelSelection()
    {
        P01ClickAudioSource.Play();
        SceneManager.LoadScene("LevelSelection");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButtonBeha : MonoBehaviour ,IPointerClickHandler{

    GameController c ;
    bool state ;
    Image currentImage ;

    private Sprite Run;
    private Sprite Stop;

    // Use this for initialization
    void Start ()
    {
        c = GameObject.Find("Controller").GetComponent<GameController>();
        currentImage = gameObject.GetComponent<Image>();

        Run = Resources.Load("Level/Button/run", typeof(Sprite)) as Sprite;
        Stop = Resources.Load("Level/Button/stop", typeof(Sprite)) as Sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        state = c.IsGameOn;

        if (state)
        {
            c.StopGame();
            currentImage.sprite = Run;
        }
        else 
        {
            c.StartGame();
            currentImage.sprite = Stop;
        }
    }

    public void StopGameDueToCrash()
    {
        StartCoroutine(resetImage());
    }

    IEnumerator resetImage()
    {
        yield return  new WaitForEndOfFrame();
        currentImage.sprite = Run;
    }
}

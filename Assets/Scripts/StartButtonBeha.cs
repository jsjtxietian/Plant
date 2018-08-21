using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButtonBeha : MonoBehaviour ,IPointerClickHandler{

    GameController c ;
    bool state ;
    Image currentImage ;

    // Use this for initialization
    void Start ()
    {
        c = GameObject.Find("Controller").GetComponent<GameController>();
        currentImage = gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        state = c.IsGameOn;

        if (state)
        {
            c.StopGame();
            currentImage.sprite = Resources.Load("Level/Button/run", typeof(Sprite)) as Sprite;
        }
        else
        {
            c.StartGame();
            currentImage.sprite = Resources.Load("Level/Button/stop", typeof(Sprite)) as Sprite;
        }
    }

    public void StopGameDueToCrash()
    {
        currentImage.sprite = Resources.Load("Level/Button/run", typeof(Sprite)) as Sprite;
    }
}

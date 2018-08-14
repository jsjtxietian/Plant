using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    private VideoPlayer thisVideo;
    private bool isStart;

    public GameObject ReplayUI;

    // Use this for initialization
    void OnEnable ()
    {
        thisVideo = gameObject.GetComponent<VideoPlayer>();
        isStart = false;
        StartCoroutine(StartPlay());
    }

    void OnDisable()
    {
        thisVideo.Stop();
    }

    void Update()
    {
        if (isStart)
        {
            if (thisVideo.frame == (long)thisVideo.frameCount)
            {
                thisVideo.Stop();
                ReplayUI.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator StartPlay()
    {
        thisVideo.Prepare();

        while (!thisVideo.isPrepared)
        {
            yield return new WaitForEndOfFrame();
        }

        thisVideo.Play();
        isStart = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTest : MonoBehaviour
{

    public string url;

    [SerializeField]
    AbstractVideoPlayer vid;
    // Start is called before the first frame update
    void Start()
    {

        vid = GetComponent<AbstractVideoPlayer>();
        vid.videoStartedEvent.AddListener(VideoStarted);
        vid.videoEndedEvent.AddListener(OnVideoEnded);
    }
    public void VideoStarted ()
    {
        Debug.Log("video started");
    }
    public void OnVideoEnded()
    {
        Debug.Log("video ended");

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            vid.PauseVideo();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            vid.PlayVideo(url);

        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            vid.ResumeVideo();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            vid.StopVideo();
        }
    }

  
}

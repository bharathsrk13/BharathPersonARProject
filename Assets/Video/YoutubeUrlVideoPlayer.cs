using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// use this script to stream youtube player.
/// This script uses YoutubeAdapter which service like adapter between third party library and this script
/// </summary>
[RequireComponent(typeof(YoutubeAdapter))]
public class YoutubeUrlVideoPlayer : AbstractVideoPlayer
{
     YoutubeAdapter adapter;

    // Start is called before the first frame update
    void Awake()
    {
        // init will initialize required variables for our video player
        Init();

        adapter = GetComponent<YoutubeAdapter>();
        adapter.youtubeVideoStartedEvent.AddListener(OnVideoStarted);
    }
   
    public override void PauseVideo()
    {
        adapter.PauseVideo();
    }

    public override void PlayVideo(string str)
    {
        URL = str;
        Logger.Log("URL is " + URL);
        adapter.PlayVideo(URL);
    }

    public override void ResumeVideo()
    {
        adapter.ResumeVideo();
    }

    public override void StopVideo()
    {
        adapter.StopVideo();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}

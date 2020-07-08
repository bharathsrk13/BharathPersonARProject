using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoutubePlayer;
/// <summary>
/// This call serves as adapter between youtube library's youtubeplayer and our custom class
/// Adapter patter
/// </summary>
public class YoutubeAdapter : YoutubePlayer.YoutubePlayer
{

    public void PlayVideo(string url)
    {
        PlayYoutubeVideo(url);
    }
    public void PauseVideo()
    {
        videoPlayer.playbackSpeed = 0;
        audioSource.Pause();
    }
    public void ResumeVideo()
    {
        videoPlayer.playbackSpeed = 1;
        audioSource.Play();
    }
    public void StopVideo()
    {
        videoPlayer.Stop();
        audioSource.Stop();
    }
}

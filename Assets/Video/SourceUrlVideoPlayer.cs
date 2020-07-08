﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

//
public class SourceUrlVideoPlayer : AbstractVideoPlayer


{

    MeshRenderer meshRenderer;

   
    private void Start()
    {
        Init();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void PauseVideo()
    {
        videoPlayer.playbackSpeed = 0f;
    }

    public override void ResumeVideo()
    {
        videoPlayer.playbackSpeed = 1f;
    }

    public override void PlayVideo(string str)
    {
        URL = str;
        Logger.Log("URL " + str);
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.Url;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        // videoPlayer.clip = videoToPlay;
        videoPlayer.url = URL;
        videoPlayer.Prepare();


        WaitForSeconds waitTime = new WaitForSeconds(3f);
        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return waitTime;
            break;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        meshRenderer.material.mainTexture = videoPlayer.texture;

        OnVideoStarted();
        //Play Video
        videoPlayer.Play();
        meshRenderer.enabled = true;
        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }
    public override void StopVideo()
    {
       // meshRenderer.enabled = false;
        videoPlayer.Stop();
        audioSource.Stop();
    }

    
}

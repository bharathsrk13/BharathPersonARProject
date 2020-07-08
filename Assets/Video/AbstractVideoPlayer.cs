using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public abstract class AbstractVideoPlayer : MonoBehaviour

{
   /// <summary>
   /// add your methods as a listener if you want to get notified once the playback started
   /// </summary>
   public UnityEvent videoStartedEvent;

    /// <summary>
    /// Add your methods as a listener if you want to get notified once video finished playing
    /// </summary>
    public UnityEvent videoEndedEvent;
   
   protected VideoPlayer videoPlayer;
   protected AudioSource audioSource;
    protected string URL;

    /// <summary>
    /// To play video
    /// </summary>
    /// <param name="url_str"> any video url that you want to get played</param>
   public abstract void PlayVideo(string url_str);

    /// <summary>
    /// To pause video
    /// </summary>
   public abstract void PauseVideo();

/// <summary>
/// To resume video
/// </summary>
   public abstract void ResumeVideo();

    /// <summary>
    /// To stop video
    /// </summary>
   public abstract void StopVideo();

public VideoPlayer GetVideoPlayer
    {
        get { return videoPlayer; }
    }
        /// <summary>
        /// This part will handle initializing vid, audiosource and other variables
        /// </summary>
   public virtual void Init()
    {
        if(GetComponent<VideoPlayer>()==null)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }
        else
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }


        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }
        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoEnded;
    }
   protected virtual void OnVideoEnded(VideoPlayer v)
    {
        videoEndedEvent.Invoke();
    }
    protected virtual void OnVideoStarted()
    {
        videoStartedEvent.Invoke();
    }

    /// <summary>
    /// you can release all the video allocations with this method
    /// </summary>
   public virtual void ReleaseAllVideoAllocations()
    {
        videoPlayer.Stop();
        audioSource.Stop();
       
        Object.Destroy(videoPlayer);
        Object.Destroy(audioSource);

        videoPlayer = null;
        audioSource = null;
      
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        Logger.Log("Video memory released");
    }

}

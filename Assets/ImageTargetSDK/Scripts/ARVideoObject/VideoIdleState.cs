using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoIdleState : AbstractVideoState
{
    public VideoIdleState(ARVideoObject v) : base(v)
    {

    }
    public override IEnumerator PlayVideo()
    {
       
        arVideoObj.SetState(arVideoObj.videoPlaybackInitiatedState);
        arVideoObj.VideoPlayerObj.PlayVideo(arVideoObj.VideoUrl);

        //arVideoObj.VideoPlayerObj.videoStartedEvent.AddListener(VideoStartedPlaying);


        yield return null; 
    }
    //TODO Remove this
    //void VideoStartedPlaying()
    //{
    //    arVideoObj.SetState(arVideoObj.playingState);
    //}

    //private void OnDestroy()
    //{
    //    arVideoObj.VideoPlayerObj.videoStartedEvent.RemoveListener(VideoStartedPlaying);

    //}


    public override IEnumerator Start()
    {
        Logger.Log("video idle state : " + arVideoObj.gameObject.transform.parent.gameObject.name);

        arVideoObj.OnOffLoadingIndicator(false);

        arVideoObj.OnOffARObjUI(false);

        arVideoObj.TurnOnOffRedirectTemplate(false);
        arVideoObj.GetMeshRenderer.enabled = false;
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayingState : AbstractVideoState
{
    public VideoPlayingState( ARVideoObject arObj) : base (arObj)
    {

    }
    public override IEnumerator PauseVideo()
    {
        arVideoObj.VideoPlayerObj.PauseVideo();
        arVideoObj.SetState(arVideoObj.pausedState);

        arVideoObj.TurnOnOffVideoProgressbar(false);
        yield return null;
    }
   


    // Start is called before the first frame update
    public override IEnumerator Start()
    {
        Logger.Log("video playing state : " + arVideoObj.gameObject.transform.parent.gameObject.name);

        arVideoObj.OnOffLoadingIndicator(false);
        //turn on mesh renderrer while playing
        arVideoObj.GetMeshRenderer.enabled = true;
        //
        arVideoObj.OnOffARObjUI(true);

        arVideoObj.TurnOnOffVideoProgressbar(true);
        Eventbus.Instance.TriggerEvent(SystemStatus.Tracking);
        
        yield return null;
    }

    
}

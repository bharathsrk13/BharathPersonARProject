using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlaybackInitiatedState : AbstractVideoState
{
    public VideoPlaybackInitiatedState (ARVideoObject v) : base (v)
    {

    }
    
  
    public override IEnumerator Start()
    {
        Logger.Log("video playback initated");
        arVideoObj.OnOffLoadingIndicator(true);

        Eventbus.Instance.TriggerEvent(SystemStatus.Tracking);
        yield return null;
    }
}

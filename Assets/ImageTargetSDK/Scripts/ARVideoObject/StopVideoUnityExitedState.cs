using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopVideoUnityExitedState : AbstractVideoState
{
    public StopVideoUnityExitedState(ARVideoObject obj) : base(obj)
    {

    }
    public override IEnumerator Start()
    {
        arVideoObj.VideoPlayerObj.ReleaseAllVideoAllocations();
        arVideoObj.GetMeshRenderer.enabled = false;
        arVideoObj.OnOffLoadingIndicator(false);

        arVideoObj.OnOffARObjUI(false);

        Logger.Log("hey unity exited pause all video");
        yield return null;
    }
   
}

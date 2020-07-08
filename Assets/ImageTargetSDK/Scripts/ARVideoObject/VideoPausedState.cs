using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPausedState : AbstractVideoState
{
    public VideoPausedState(ARVideoObject obj) : base(obj)
    {

    }
    public override IEnumerator PlayVideo()
    {
        arVideoObj.VideoPlayerObj.ResumeVideo();
        arVideoObj.SetState(arVideoObj.playingState);
        yield return null;
    }
    public override IEnumerator Start()
    {
        //TODO delimiter anti pattern may be add turnOnOFfMeshrenderer() in arobj
        arVideoObj.GetMeshRenderer.enabled = false;
        arVideoObj.OnOffARObjUI(false);
        yield return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoEndedState : AbstractVideoState
{
    public VideoEndedState(ARVideoObject obj): base (obj)
    {

    }
    public override IEnumerator Start()
    {
        arVideoObj.TurnOnOffRedirectTemplate(true);
        arVideoObj.GetMeshRenderer.enabled = false;
        Logger.Log("video ended state");
        yield return null; 
    }
}

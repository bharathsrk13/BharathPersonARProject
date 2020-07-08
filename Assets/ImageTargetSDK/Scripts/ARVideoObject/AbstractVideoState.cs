using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public abstract class AbstractVideoState : MonoBehaviour
{
   protected ARVideoObject arVideoObj;

    
   
   public AbstractVideoState(ARVideoObject arObj)
    {
        this.arVideoObj = arObj;
        
    }

    public virtual IEnumerator Start()
    {
        yield return null;
    }

    public virtual IEnumerator PlayVideo()
    {
        yield return null;
    }

    public virtual IEnumerator PauseVideo()
    {
        yield return null;
    }

   
    public virtual IEnumerator StopVideo()
    {
        arVideoObj.VideoPlayerObj.StopVideo();
        //    arVideoObj.SetState(new StopVideoUnityExitedState());
        arVideoObj.TurnOnOffVideoProgressbar(false);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public abstract class AbstractHUDElements : MonoBehaviour
{
   // protected SystemStatus currentSystemStatus;
    // Start is called before the first frame update
    public void Start()
    {
      //  GetComponent<ARTrackedImageManager>().trackedImagesChanged += trackedImagesChanged;
        Eventbus.Instance.StartListenSystemStatusChange(OnimageTargetStatechanged);
    }

    public void OnDestroy()
    {
       // GetComponent<ARTrackedImageManager>().trackedImagesChanged -= trackedImagesChanged;
        Eventbus.Instance.StopListeningStatusChange(OnimageTargetStatechanged);

    }
   

    protected abstract void OnimageTargetStatechanged(SystemStatus systemStatus);
}

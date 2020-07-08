using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : AbstractHUDElements
{
    [SerializeField]
    GameObject loadingScreen;


    protected override void OnimageTargetStatechanged(SystemStatus systemStatus)
    {
        //TODO remove this log
        Logger.Log("image target state changed : from loading screen");
#if Unity_ios
        Logger.Log("ios compiled rightly");
        if(systemStatus == SystemStatus.SettingUpImageLibrary)
        {
            loadingScreen.SetActive(true);
            Logger.Log("turning on loadingScreen panel");

        }
        else if(systemStatus == SystemStatus.ReadyForTracking)
        {
            loadingScreen.SetActive(false);
        }
#endif

    }
}

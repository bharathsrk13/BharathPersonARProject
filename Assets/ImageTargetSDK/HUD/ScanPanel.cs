using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPanel : AbstractHUDElements
{
    public GameObject scanPanel;
        
    protected override void OnimageTargetStatechanged(SystemStatus systemStatus)
    {
        Logger.Log("on status changed call back is working" + systemStatus);
if(systemStatus == SystemStatus.ReadyForTracking)
        {
            scanPanel.SetActive(true);
            Logger.Log("turning on scan panel");
        }
else
        {
            Logger.Log("turnign off scan panel");
            scanPanel.SetActive(false);
        }
    }

    
}

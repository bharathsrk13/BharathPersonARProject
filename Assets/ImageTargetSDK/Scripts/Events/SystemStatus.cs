using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SystemStatus
{
    SettingUpImageLibrary,
    DownloadFailed,
    ImageAddedToLibrary,
    FailedToAddToLibrary,
    ReadyForTracking,
    Tracking,
    OnUnityExit
}

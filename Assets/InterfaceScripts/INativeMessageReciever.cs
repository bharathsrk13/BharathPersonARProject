using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INativeMessageReciever
{
    /// <summary>
    /// Get Json and parse to required format
    /// </summary>
    /// <param name="json"></param>
    void JsonFromLifeCycleManager(string json);

}

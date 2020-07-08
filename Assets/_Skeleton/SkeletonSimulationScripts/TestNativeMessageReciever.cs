using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNativeMessageReciever : MonoBehaviour, INativeMessageReciever
{
    public void JsonFromLifeCycleManager(string json)
    {
        Logger.Log("message recieved : "+json);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

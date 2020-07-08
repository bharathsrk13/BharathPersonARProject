using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkeletonTest : MonoBehaviour
{
    //get a json from url

    public string jsonURL;
    string message;

    public void Start()
    {
       //StartCoroutine( GetJsonFromURL());
    }

    IEnumerator GetJsonFromURL()
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonURL);
        yield return www.SendWebRequest();

        message = www.downloadHandler.text;
        LoadImageTarget();
    }
    /// <summary>
    /// use this method to load image target asyncly with just message to be sent image targets as parameter
    /// </summary>
    /// <param name="msg"></param>
    public void LoadAndSendMessageToImageTarget(string msg)
    {
        message = msg;
        LoadImageTarget();
        
    }
    public void LoadImageTarget()
    {
        LoadModule("ImageTarget");
        SendMessage();
    }

    public void btn2()
    {
        UnloadModule();
    }

    public void btn3()
    {
        LoadModule("360");
        SendMessage();
    }
    public void btn4()
    {
        LoadModule("ImageTarget");
    }
    public void LoadModule(string str)
    {
        GetComponent<LifeCycleManager>().LoadModule(str);
    }
    public void UnloadModule()
    {
        GetComponent<LifeCycleManager>().UnloadModules();

    }
    public void SendMessage()
    {
        GetComponent<LifeCycleManager>().JsonFromNative(message);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadImageTarget();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            btn2();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            btn3();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            btn4();
        }
    }
}

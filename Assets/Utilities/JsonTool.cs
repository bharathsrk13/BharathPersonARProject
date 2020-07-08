using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class JsonTool<T> : MonoBehaviour
{


 //   public T ParseJson(string str )
	//{

 //       T data = JsonUtility.FromJson<T>(str);
	//	return data;
	//}
    /// <summary>
    /// This function can retireve json,parse and return as the type of T (use this for debugging, without getting message from native we can simulate using this
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    public void GetDataFromOnline(string url,UnityAction<T> callback)
	{
        StartCoroutine(GetJsonOnline(url, callback));
	}
	IEnumerator GetJsonOnline(string URL, UnityAction<T> callBack)
	{
		UnityWebRequest www = UnityWebRequest.Get(URL);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
            Logger.Log("network or http error");
			yield break;
		}

        Logger.Log("No error");
        Logger.Log("downloaded url " + www.downloadHandler.text);
        //callBack.Invoke(ParseJson(www.downloadHandler.text));
		

	}
}

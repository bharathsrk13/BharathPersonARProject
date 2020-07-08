using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DownloadImageFromServer : MonoBehaviour,IDataSender,IDataReciever<ImageVideoTemplate>
{
    [SerializeField]
    private Texture2D currentDownloadedImage;

    //we will recieve image url template and start downloading image from the image url in imageurl template
    public void RecieveData(ImageVideoTemplate data)
    {
        StartCoroutine(DownloadImageFromUrl(data.targetImageUrl));
    }

    public void SendData()
    {
      //  Logger.Log("testing todo remove this sedning data to addimagetolibrary");
       GetComponent<IDataReciever<Texture2D>>().RecieveData(currentDownloadedImage);
    }

    private IEnumerator DownloadImageFromUrl(string imageUrl )
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return webRequest.SendWebRequest();

        Logger.Log("request sent");

        //if request failed
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Logger.Log("network error");
            webRequest.Dispose();

            //trigger download failed event
            Eventbus.Instance.TriggerEvent(SystemStatus.DownloadFailed);
        }
        // successfully downloaded image
        else
        {
            //clean previously downloaded image from memory and flush reference from GC
            currentDownloadedImage = null;
            Resources.UnloadUnusedAssets();
            GC.Collect();

            currentDownloadedImage = DownloadHandlerTexture.GetContent(webRequest);
            
            webRequest.Dispose();
            

            //send data (texture2D) to add image to library
            SendData();
        }
    }
}

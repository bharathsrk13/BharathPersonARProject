using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPrefabFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject videoPrefab;

    /// <summary>
    /// Generate ARVideoObj based on specifications (input params(
    /// </summary>
    /// <param name="imageTargetTemplate">image target template</param>
    /// <param name="objectName"> name to be given for object going to be generated (instantiated) </param>
    /// <returns></returns>
    
    public GameObject CreateARVideoObject(ImageVideoTemplate imageTargetTemplate, string objectName)
    {
        //instantiate object
        GameObject go = Instantiate<GameObject>(videoPrefab);
        go.name = objectName;

        //in arobj component set video url
        var arobj = go.GetComponentInChildren<ARVideoObject>();
        arobj.VideoUrl = imageTargetTemplate.targetVideoUrl;

        //arvideo obj ui is nested object reponsible for ARVideoObj's UI related stuffs, so update ui related stuffs here.
        arobj.arVideoObjUIPrefab.GetComponent<ARVideoObjUI>().SetUIData(imageTargetTemplate.adsInfo, imageTargetTemplate.adsRedirectUrl);
        //TODO delete ads redirect url comment line below
        Logger.Log("In factory testing ads redirect url "+imageTargetTemplate.adsRedirectUrl);

        Logger.Log("video obj instantiated");
        return go;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
public class ARVideoObjUI : MonoBehaviour
{
    [SerializeField]
    private AbstractVideoPlayer av;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private string redirectWebURL;
    [SerializeField]
    private bool canUpdateSlider;

    [SerializeField]
    private Slider videoProgressSlider;

    [SerializeField]
    private TextMeshProUGUI redirectText;


    [SerializeField]
    private GameObject redirectTemplate;
    
    /// <summary>
    /// this is used to set ARVideoobj ui, mostly used by videoprefab factory while manufactoring this
    /// </summary>
    /// <param name="redirectDescription"></param>
    /// <param name="redirectURL"></param>
    public void SetUIData(string redirectDescription, string redirectURL)
    {
       
        redirectText.text = redirectDescription;
        redirectWebURL = redirectURL;
        Logger.Log("Ads redirect url on arobjui" + redirectWebURL);
    }

    public void RedirectToWebPage()
    {
        Application.OpenURL(redirectWebURL);
        Logger.Log("opening url:" + redirectWebURL);
    }
    public void ResetTracking()
    {
        Eventbus.Instance.TriggerEvent(SystemStatus.ReadyForTracking);
        Logger.Log("resetting tracking");
    }
    public void OnOffVideoProgressBar(bool val)
    {
        videoProgressSlider.enabled = val;
        canUpdateSlider = val;
    }
    public void Initialise(AbstractVideoPlayer av)
    {
        print(av);
        this.av = av;

        videoPlayer = av.GetVideoPlayer;
        if(videoPlayer==null)
        {
            print("shit");
        }
        
    }

   public void OnOffRedirectTemplate(bool val)
    {
        redirectTemplate.SetActive(val);
    }
    public void Update()
    {
        if(canUpdateSlider)
        {
            UpdateSlider();

        }
    }
    public void UpdateSlider()
    {
        var roundedVideoProgressValue  = (float)System.Math.Round(videoPlayer.time / videoPlayer.length, 2);
        Logger.Log("slider value" + roundedVideoProgressValue);
        if(!float.IsNaN(roundedVideoProgressValue))
        {
            Logger.Log("slider proper value" + roundedVideoProgressValue);

            videoProgressSlider.value = roundedVideoProgressValue;
        }
       
    }


}

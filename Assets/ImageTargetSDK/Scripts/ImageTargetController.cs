using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImageTargetController : MonoBehaviour,INativeMessageReciever,IController, IDataSender
{
    [SerializeField]
    ImageVideoTemplate imageVideoTemplate;
    VideoPrefabFactory factory;
    string currentImageURl;
    private List<string> imageUrlsAddedToLibrary = new List<string>();

   
    // Start is called before the first frame update
    void Start()
    {
        factory=GameObject.FindObjectOfType<VideoPrefabFactory>();
    }

    void OnDestroy()
    {
    }

    /// <summary>
    /// this is the entry point for gettng message from lifecycle manager in adapter scen
    /// </summary>
    /// <param name="json"></param>
    public void JsonFromLifeCycleManager(string json)
    {
        Logger.Log(json);

        //parsing json
        ImageVideoTemplateCollection imageVideoTemplateCollection = new ImageVideoTemplateCollection();
        imageVideoTemplateCollection = JsonUtility.FromJson<ImageVideoTemplateCollection>(json);

        //assign image url from parsed json to currentImageURL
        currentImageURl = imageVideoTemplateCollection.arList[0].targetImageUrl;

        if (imageUrlsAddedToLibrary.Contains(currentImageURl))
        {
            Logger.Log("image already downloaded now ready for tracking");
            OnReadyForTracking();
        }
        else
        {
            Logger.Log("image not already downloaded now initiating download and library addition process");


            imageVideoTemplate = imageVideoTemplateCollection.arList[0];

            Logger.Log(imageVideoTemplateCollection.arList[0].targetImageUrl);


            SendData();
        }
      

    }

    public void SendData()
    {

        Eventbus.Instance.TriggerEvent(SystemStatus.SettingUpImageLibrary);
        GetComponent<IDataReciever<ImageVideoTemplate>>().RecieveData(imageVideoTemplate);

    }

    public void OnBackPressed()
    {
        Eventbus.Instance.TriggerEvent(SystemStatus.OnUnityExit);
        Logger.Log("back pressed");
    }
    
    public void OnImageAddedToLibrary(string imageID)
    {
        Logger.Log("controller recieved image added to library event "+imageID);

        //if the image successfully added to library add current image url to downloaded urls list
        imageUrlsAddedToLibrary.Add(currentImageURl);

        //now we ready for tracking so fire onreadyfortracking event
        OnReadyForTracking();

        //now create video prefab object from factory
        factory.CreateARVideoObject(imageVideoTemplate,imageID);
        
    }
   
    void OnReadyForTracking()
    {
        Eventbus.Instance.TriggerEvent(SystemStatus.ReadyForTracking);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnReadyForTracking();
            Logger.Log("ready for tracking fired");
        }
#endif
    }
}

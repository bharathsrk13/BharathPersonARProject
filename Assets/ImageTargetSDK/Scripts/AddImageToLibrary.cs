using System.Collections;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class AddImageToLibrary : MonoBehaviour, IDataReciever<Texture2D>

{
    private ARTrackedImageManager trackedImageManager;
    private ImageTargetController imageTargetController;
    [SerializeField]
    private Texture2D currentTexture;
    private int addedImageCount;


    public void RecieveData(Texture2D data)
    {
        currentTexture = data;
        StartCoroutine(AddImageJob());
    }

    public void Awake()
    {
      trackedImageManager = GameObject.FindObjectOfType<ARSessionOrigin>().GetComponent<ARTrackedImageManager>();
        imageTargetController = GetComponent<ImageTargetController>();
    }
    private IEnumerator AddImageJob()
    {
        yield return null;



        Logger.Log("Adding image\n");

        Logger.Log("Job Starting...");

        var firstGuid = new SerializableGuid(0, 0);
        var secondGuid = new SerializableGuid(0, 0);

        //   XRReferenceImage newImage = new XRReferenceImage(firstGuid, secondGuid, new Vector2(0.1f, 0.1f), Guid.NewGuid().ToString(), texture2D);
        addedImageCount++;
        XRReferenceImage newImage = new XRReferenceImage(firstGuid, secondGuid, new Vector2(0.1f, 0.1f), addedImageCount.ToString(), currentTexture);

        try
        {
            // loadingGameObj.SetActive(true);

            // Debug.Log("new image naame"+newImage.ToString()) ;

            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = trackedImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;



            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(currentTexture, addedImageCount.ToString(), 0.1f);

            while (!jobHandle.IsCompleted)
            {
                Logger.Log ("Job Running...");
            }
            Logger.Log("job finsihed added to dictionary");

            //remove things from memory
            currentTexture = null;
            Resources.UnloadUnusedAssets();
            GC.Collect();

            //logging
            Logger.Log("image downloaded and added to library");
            Logger.Log("image count" + mutableRuntimeReferenceImageLibrary.count);

            //trigger event to convey success message
            Eventbus.Instance.TriggerEvent(SystemStatus.ImageAddedToLibrary);
            //tell the controller that you had added image to library and send this id to controller
            imageTargetController.OnImageAddedToLibrary(newImage.name);

        }
        catch (Exception e)
        {
            Eventbus.Instance.TriggerEvent(SystemStatus.FailedToAddToLibrary);
            Logger.Log(e.ToString());
        }
    }
   
}

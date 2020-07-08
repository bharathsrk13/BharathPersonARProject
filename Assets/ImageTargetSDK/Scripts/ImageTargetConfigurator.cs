using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// This script will add ARTrackedImageManager at runtime and responsible for solving odd detection rotation issue
/// attach this to ar session origin (ethunalum work aagum but use it on arsessionorigin to maintain consistencies)
/// </summary>
	public class ImageTargetConfigurator : MonoBehaviour
	{
		[SerializeField]
		protected GameObject prefabOnTrack;

		
		[SerializeField]
		protected XRReferenceImageLibrary runtimeImageLibrary;

		protected ARTrackedImageManager trackImageManager;

        int count;
        List<ARTrackedImage> totalTrackedImages = new List<ARTrackedImage>();
        //created this list to rotate only images added in odd number wise eg element added as 1st 3rd and 5th etc
        List<ARTrackedImage> oddList = new List<ARTrackedImage>();
		
		protected virtual void Awake()
		{
			Logger.Log("Creating Runtime Mutable Image Library\n");
        if(GetComponent<ARTrackedImageManager>()==null)
        {
            trackImageManager = gameObject.AddComponent<ARTrackedImageManager>();

        }
        else
        {
            trackImageManager = GetComponent<ARTrackedImageManager>();

        }
        trackImageManager.maxNumberOfMovingImages = 1;
            //todo not addding prefab via artrackedimagemanager
			//trackImageManager.trackedImagePrefab = prefabOnTrack;

			

			trackImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        // ShowTrackerInfo();
        trackImageManager.enabled = false;
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);
        trackImageManager.enabled = true;


    }


		void Destroy()
		{
			trackImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
		}

        bool test = true;

		void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
		{
        /*
            totalTrackedImages.AddRange(eventArgs.added);
            Logger.Log("total tracked image count " + totalTrackedImages.Count);
            for (int i=0;i<totalTrackedImages.Count;i++)
            {
                if(i%2 !=0)
                {
                    if(!oddList.Contains(totalTrackedImages[i]))
                    oddList.Add(totalTrackedImages[i]);

                    Logger.Log("odd list count"+oddList.Count);
                }
            }
            foreach (ARTrackedImage trackedImage in eventArgs.added)
            {
                
                //print("Image has been tracked successfully" + trackedImage.name);

                //trackedImage.transform.Rotate(Vector3.up, 180);
                if(oddList.Contains(trackedImage))
                {
                    Logger.Log("image in odd list");
                    trackedImage.transform.Rotate(Vector3.up, 180);
                }

            }

            foreach (ARTrackedImage trackedImage in eventArgs.updated)
            {

                // trackedImage.transform.Rotate(Vector3.up, 180);
                if (oddList.Contains(trackedImage))
                {
                    Logger.Log("image in odd list updated");

                    trackedImage.transform.Rotate(Vector3.up, 180);
                }
            }*/
        }
    }

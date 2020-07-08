using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum PlayMode
{
    EditorMode,
    ARMode
}
public class ARVideoObject : StateMachine
{
    private SystemStatus currentSystemStatus;

    [SerializeField]
    private AbstractVideoPlayer videoPlayer;
    

     string videoUrl;
    [SerializeField]
    GameObject loadingIndicator;

    public GameObject arVideoObjUIPrefab;
	ARVideoObjUI arUI;

    [HideInInspector]
    public VideoIdleState idleState;

    [HideInInspector]
    public VideoPlayingState playingState;

    [HideInInspector]
    public VideoPausedState pausedState;

    [HideInInspector]
    public VideoEndedState endedState;

    [HideInInspector]
    public StopVideoUnityExitedState unityExitedState;

    [HideInInspector]
    public VideoPlaybackInitiatedState videoPlaybackInitiatedState;

    private MeshRenderer meshRenderer;
    public AbstractVideoPlayer VideoPlayerObj
    {
        get { return videoPlayer; }
    }
    public MeshRenderer GetMeshRenderer
    {
        get { return meshRenderer; }
    }
    public string VideoUrl
    {
        get { return videoUrl; }
        set { videoUrl = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<AbstractVideoPlayer>();
        videoPlayer.videoEndedEvent.AddListener(OnVideoLoopEnded);

        meshRenderer = GetComponent<MeshRenderer>();
#if !UNITY_EDITOR
        {
            //TODO uncomment tracked images changed
            GameObject.FindObjectOfType<ARTrackedImageManager>().trackedImagesChanged += OnTrackedImagesChanged;
        }
#endif
        //TODO this is temp solution delete this line in future videostarted events add listener method
        //once the video finished processing and getting ready for playing this event will be fire and we will set state to play state
        VideoPlayerObj.videoStartedEvent.AddListener(OnVideoStartedPlaying);

        //instantiate canvas prefab and cache reference to its ARVideoObjUI
        arUI = Instantiate<GameObject>(arVideoObjUIPrefab,transform.parent).GetComponent<ARVideoObjUI>();
        arUI.Initialise(VideoPlayerObj);

        Eventbus.Instance.StartListening(SystemStatus.OnUnityExit, OnUnityExited);
        Eventbus.Instance.StartListening(SystemStatus.ReadyForTracking, OnReadyForTracking);

        //initialise video state variables instead of creating them using new keyword we can reuse these variables
        InitialiseVideoStates();
        

        //in the start set current state as idle state
        SetState(idleState);

    }
    private void OnDestroy()
    {
        Eventbus.Instance.StopListening(SystemStatus.OnUnityExit, OnUnityExited);
        Eventbus.Instance.StartListening(SystemStatus.ReadyForTracking, OnReadyForTracking);

        #if !UNITY_EDITOR

        {
            //TODO uncomment tracked images changed
            GameObject.FindObjectOfType<ARTrackedImageManager>().trackedImagesChanged -= OnTrackedImagesChanged;
        }
        #endif
        //TODO remove remove listener method
        VideoPlayerObj.videoStartedEvent.RemoveListener(OnVideoStartedPlaying);


    }
    public void OnOffARObjUI(bool val)
    {
        arUI.gameObject.SetActive(val);
    }

    public void OnOffLoadingIndicator(bool val)
    {
        loadingIndicator.SetActive(val);
    }
   
    /// <summary>
	/// To turn on and off video playback progress
	/// </summary>
	/// <param name="val"> true for turning on and false for turning off</param>
    public void TurnOnOffVideoProgressbar(bool val)
	{
		arUI.OnOffVideoProgressBar(val);
	}

    public void TurnOnOffRedirectTemplate(bool val)
    {
        arUI.OnOffRedirectTemplate(val);
    }
    #region Setting states
    private void OnVideoStartedPlaying()
    {
        SetState(playingState);
    }

    private void OnReadyForTracking()
    {
        SetState(idleState);
    }
    private void OnVideoLoopEnded()
    {
        SetState(endedState);
    }

    private void OnUnityExited()
    {
        SetState(unityExitedState);
    }
#endregion

    private void InitialiseVideoStates()
    {
        //initialise state variables here so , state machines no need to create new states everytime using new keyword.we
        //can resuse initialised sates 
        idleState = new VideoIdleState(this);
        playingState = new VideoPlayingState(this);
        pausedState = new VideoPausedState(this);
        endedState = new VideoEndedState(this);
        unityExitedState = new StopVideoUnityExitedState(this);
        videoPlaybackInitiatedState = new VideoPlaybackInitiatedState(this);
    }
    private Vector3 calculateScale(Vector2 val)
    {
        var median = val.x / 16;
        var newX = 16 * median;
        var newY = 9 * median;
        print("size resizer ends with x " + newX + " z" + newY);

        return new Vector3(newX, newY, 1f);
    }
    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        Logger.Log("this shit is working");

        foreach (ARTrackedImage image in obj.updated)
        {
            Logger.Log("image name " + image.name);
            if (image.referenceImage.name == transform.parent.gameObject.name)
            {
                Logger.Log("name matched " + "image name " + image.referenceImage.name + "parent name " + transform.parent.gameObject.name);
                if (image.trackingState == TrackingState.Tracking)
                {

                    transform.parent.position = image.transform.position;
                    transform.parent.rotation = image.transform.rotation;
                    transform.localScale = calculateScale(new Vector2(image.size.x, image.size.y));

                    StartCoroutine(currentVideoState.PlayVideo());

                }

                else
                {

                    StartCoroutine(currentVideoState.PauseVideo());

                }
            }

        }
    }

    private void Update()
    {
        //for simulating arvideoobj inside unity
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(currentVideoState.PlayVideo());
        }
        else if( Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(currentVideoState.PauseVideo());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnUnityExited();
        }
#endif
    }
}

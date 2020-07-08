using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Lifecycle manager is responsible for managing life cycle of AR modules
/// like loading and unloading modules and recieving messages from native side
/// </summary>
public class LifeCycleManager : MonoBehaviour
{
    [SerializeField]
    private string bufferedMessage;
    private string currentLoadedModule;
    

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadScenemode)
    {
        Logger.Log(scene.name+" module loaded");
        if(!string.IsNullOrEmpty( bufferedMessage))
        {
            Logger.Log("current message sending to sdk " + bufferedMessage);
            //once the scene has been loaded we were checking any message left to send
            // because from native messages will be sent one after other, so there were some chances that message will
            //be sent to modules even before the scene loaded completely. so we buffering those messages and sent back to the
            //respective scene once scene loaded completely
            SendMessageToModule(bufferedMessage);
        }
    }
    /// <summary>
	/// This function can load AR modules (scenes) based on modulenName Input
	/// primarily used to recieve load message call from native and to load required AR module
	/// </summary>
	/// <param name="moduleName"></param>
    public void LoadModule(string moduleName)
    {
        if(moduleName == currentLoadedModule)
        {
            Logger.Log("module already loaded");
        }
        else
        {
            UnloadModules();
            //Load scene async
            Logger.Log("loading module" + moduleName);
            SceneManager.LoadSceneAsync(moduleName, LoadSceneMode.Additive);

            //now keep track of current scene
            currentLoadedModule = moduleName;
        }

    }
    

    /// <summary>
    /// Unload all the loaded modules
    /// </summary>
    public void UnloadModules()
    {
        //check whether there is any modules that is already loaded
        if (!string.IsNullOrEmpty(currentLoadedModule))
        {

            DeleteEntireSceneFromMemory(currentLoadedModule);
            currentLoadedModule = "";
        }
    }

    /// <summary>
	/// This will be used to recive message from native except load modules message and
	/// forwards message to currently active or loaded module
	/// </summary>
	/// <param name="message"></param>
    public void JsonFromNative (string message)
    {
        if(string.IsNullOrEmpty( currentLoadedModule))
        {
            Logger.Log("no module loaded, first load module before sending data or message");
          
        }
        else
        {
            //so there were some module loaded
            SendMessageToModule(message);
        }
    }
    /// <summary>
    /// Most efficient way of deleting objects without memory footprint
    /// </summary>
    /// <param name="sceneToDestroy"></param>
    private void DeleteEntireSceneFromMemory(string sceneToDestroy)
    {
        GameObject[] _rootGameObjectsOfSpecificScene = SceneManager.GetSceneByName(sceneToDestroy).GetRootGameObjects();
        foreach (GameObject o in _rootGameObjectsOfSpecificScene)
        {
            //ar session origin should be disable, setactive false and destroyed immediately else it will have some reference
            //in memory 
            if (o.name == "AR Session Origin")
            {
                ARSessionOrigin arorigin = o.GetComponent<ARSessionOrigin>();
                arorigin.camera.enabled = false;
                Debug.Log("AR Session Origin");
                o.gameObject.SetActive(false);
            }
            Debug.Log("Destroy" + o.name);
            DestroyImmediate(o);
        }

        //flushing objects from memory

        _rootGameObjectsOfSpecificScene = null;
        SceneManager.UnloadSceneAsync(sceneToDestroy);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

    }
    private void BufferMessage(string messageToBuffer)
	{
        bufferedMessage = messageToBuffer;
	}
    private void SendMessageToModule(string message)
    {
        if (GameObject.Find("Controller"))
        {
            bufferedMessage = message;
            GameObject.Find("Controller").GetComponent<INativeMessageReciever>().JsonFromLifeCycleManager(bufferedMessage);
            bufferedMessage = "";
        }
        else
        {
            Logger.Log("no controller object found, either module is still loading(SCENE) or controlleer wont be found in modules");
            //any how buffereing this string value in temporary string variable 

            BufferMessage(message);
        }
    }
    
}

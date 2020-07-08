using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetURLInjectorSimulator : MonoBehaviour
{
    public string[] imageTargetPlaceholderJsons;
    SkeletonTest skeletonTest;
    // Start is called before the first frame update
    void Start()
    {
        skeletonTest = GameObject.FindObjectOfType<SkeletonTest>();
    }
    public void OnBtnClicked(int index)
    {
        //this is the entry point for adapter From native this json from native method will be called
        //so we usign this same method for simulation
        skeletonTest.LoadAndSendMessageToImageTarget(imageTargetPlaceholderJsons[index]);
        Logger.Log(imageTargetPlaceholderJsons[index]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

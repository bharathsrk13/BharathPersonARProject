using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //public GameObject prefab;
    public ImageVideoTemplate imageTemplate;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<VideoPrefabFactory>().CreateARVideoObject(imageTemplate, "bh");
    //  GameObject go =  Instantiate<GameObject>(prefab);
       // go.GetComponentInChildren<ARVideoObject>().arVideoObjUIPrefab.GetComponent<ARVideoObjUI>().SetUIData("hello", "www.fanisko.com");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

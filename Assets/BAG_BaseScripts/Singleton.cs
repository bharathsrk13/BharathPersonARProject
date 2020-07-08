using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            //checking whether instance has value because suppose the gamemanager or any other singletonah hierarhcy laye add
            //panama Gamemanager.Instance.xx nu koopta awake vela seiyathu so instance null ah irukum. so checking apdi
            //scenerio irukanu
            if(instance == null)
            {
                //if is null verify is any other copy is left in hierarchy,
                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    //if instance is null and no other copies in memory create new object and add component and get that component (instance or object)
                    //for giving static class access to instance level functions..so new object will be created, renamed and GameManager will be added. So thsi
                //game manager attached to particular game object is a instance, assisn that to our instance variable and return for Instance getter var 
                    var t = new GameObject();
                    t.name = typeof(T).Name;
                    instance = t.AddComponent<T>();
                }
            }
            return instance;

        }

    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(instance==null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Diagnostics;
using UnityEngine; 

public static class Logger 
{
    [Conditional("ENABLE_LOG")]
    public static void Log(string str)
    {
        UnityEngine.Debug.Log(str);
      //  MonoBehaviour.print(str);
    }
}

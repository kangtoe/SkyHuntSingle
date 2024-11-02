using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObject = new object();
    protected static T instance = null;

    public static T Instance
    {        
        get
        {            
            lock (lockObject) // for Thread-Safe
            {                
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    
                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof(T).ToString();
                        instance = go.AddComponent<T>();                        
                    }                    
                }
                return instance;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObject = new object();
    private static T instance = null;
    private static bool IsQuitting = false;

    public static T Instance
    {
        // for Thread-Safe
        get
        {            
            lock (lockObject)
            {
                if (IsQuitting)
                {
                    return null;
                }
                
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    
                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof(T).ToString();
                        instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }

                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }

    private void OnDisable()
    {
        IsQuitting = true;
        instance = null;
    }
}
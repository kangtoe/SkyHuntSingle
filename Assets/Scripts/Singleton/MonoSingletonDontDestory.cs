using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletonDontDestory<T> : MonoSingleton<T> where T : MonoSingleton<T>
{
    new public static T Instance
    {
        get
        {            
            DontDestroyOnLoad(MonoSingleton<T>.Instance.gameObject);
            return MonoSingleton<T>.Instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(Instance.gameObject);
    }
}

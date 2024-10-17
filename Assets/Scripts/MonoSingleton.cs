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
            // 한번에 한 스래드만 lock블럭 실행
            lock (lockObject)
            {
                // 비활성화 시 새로 만든다.
                if (IsQuitting)
                {
                    return null;
                }
                
                if (instance == null)
                {
                    // 기존에 존재하는지 검사
                    instance = FindObjectOfType<T>();

                    // 없다면 새롭게 만들기
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
        // 비활성화 된다면 null로 변경
        IsQuitting = true;
        instance = null;
    }
}
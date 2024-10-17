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
            // �ѹ��� �� �����常 lock�� ����
            lock (lockObject)
            {
                // ��Ȱ��ȭ �� ���� �����.
                if (IsQuitting)
                {
                    return null;
                }
                
                if (instance == null)
                {
                    // ������ �����ϴ��� �˻�
                    instance = FindObjectOfType<T>();

                    // ���ٸ� ���Ӱ� �����
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
        // ��Ȱ��ȭ �ȴٸ� null�� ����
        IsQuitting = true;
        instance = null;
    }
}
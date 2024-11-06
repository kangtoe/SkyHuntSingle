using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoSingleton<ObjectSpawner>
{
    public GameObject SpawnObject(GameObject objectPrefab, (Vector2, Quaternion) pointAndRotation)
    {
        if (objectPrefab == null)
        {
            Debug.Log("enemyPrefab is null");
        }

        var (pos, rot) = pointAndRotation;

        GameObject go = Instantiate(objectPrefab);
        go.transform.position = pos;
        go.transform.rotation = rot;

        return go;
    }

    public void SpawnObjectsAtSide(GameObject objectPrefab, int count, Edge spawnSide)
    {
        for (int i = 1; i <= count; i++)
        {
            float lengthRatio = (float)i / (count + 1);
            var (pos, rot) = GetSpawnPointAndRotation(spawnSide, lengthRatio);

            SpawnObject(objectPrefab, (pos, rot));
        }
    }
   
    public GameObject SpawnObjectAtRandomPos(GameObject objectPrefab, List<GameObject> checkObjects)
    {
        GameObject go;
        float closeDist = 2;
        int tryCount = 100;

        do{                        
            var (pos, rot) = GetSpawnPointAndRotation();
            go = SpawnObject(objectPrefab, (pos, rot));

            tryCount--;
            if (tryCount <= 0) break;
        }
        while (CloseCheck(go, checkObjects, closeDist)); // 너무 가까우면 재배치      

        return go;
    }

    bool CloseCheck(GameObject go, List<GameObject> checkObjects, float dist)
    {
        foreach (GameObject other in checkObjects)
        {
            if ((other.transform.position - go.transform.position).magnitude < dist) return true;
        }

        return false;
    }

    (Vector2, Quaternion) GetSpawnPointAndRotation(Edge? spawnSide = null, float? lengthRatio = null)
    {
        if (spawnSide == null)
        {
            int len = Enum.GetValues(typeof(Edge)).Length;
            spawnSide = (Edge)Random.Range(0, len);
        }

        if (lengthRatio == null)
        {
            lengthRatio = Random.Range(0f, 1f);
        } 
        
        Vector3 viewPos;
        float angle;

        switch (spawnSide)
        {
            // 상부 가장자리
            case Edge.Up:
                viewPos = new Vector3(lengthRatio.Value, 1f, 1f);
                angle = 180;                
                break;

            // 하부 가장자리
            case Edge.Down:
                viewPos = new Vector3(lengthRatio.Value, 0, 1f);
                angle = 0;                
                break;

            // 오른쪽 가장자리
            case Edge.Right:
                viewPos = new Vector3(1, lengthRatio.Value, 1f);
                angle = 90;                
                break;

            // 왼쪽 가장자리
            case Edge.Left:
                viewPos = new Vector3(0, lengthRatio.Value, 1f);
                angle = 270;                
                break;

            default:
                viewPos = Vector3.zero;
                angle = 0;
                break;
        }

        Vector2 pos = Camera.main.ViewportToWorldPoint(viewPos);
        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        return (pos, rot);
    }
}

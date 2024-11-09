using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    public int min;
    public int sec;    
    public GameObject spawnPrefab;

    public int count;
    public Edge spawnSide;

    [HideInInspector] public int SpawnTime => min * 60 + sec;
}

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [SerializeField]
    GameObject defaultEnemyPrefab;

    [SerializeField]
    List<SpawnInfo> spawnInfoList;

    float ElapsedTime => TimeRecordManager.Instance.TimeRecord;

    private void Update()
    {
        for (int i = spawnInfoList.Count - 1; i >= 0; i--)
        {
            SpawnInfo spawnInfo = spawnInfoList[i];
            if (ElapsedTime > spawnInfo.SpawnTime)
            {
                ObjectSpawner.Instance.SpawnObjects(spawnInfo.spawnPrefab, spawnInfo.count, spawnInfo.spawnSide);
                spawnInfoList.RemoveAt(i);
            }
        }
    }

    private void OnValidate()
    {
        foreach (SpawnInfo info in spawnInfoList)
        {
            info.min += (int)(info.sec / 60);
            info.sec %= 60;
        }
    }

    public void AddSpawnInfo()
    {
        SpawnInfo info = new SpawnInfo();
        info.spawnPrefab = defaultEnemyPrefab;
        info.count = 1;
        info.spawnSide = Edge.Random;
        spawnInfoList.Add(info);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    [SerializeField] string desc = "Spawn Desc";

    [Space]
    public GameObject spawnPrefab;

    public float spawnTime;
    public Edge spawnSide;

    [Header("multi spawn")]
    public int count;
    public float spawnInterval;

    [HideInInspector] public float SpawnEndTime => spawnTime + count * spawnInterval;
}

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [Header("MUST SET 0 ON SHPPING")]
    [SerializeField]
    float devStartTime;

    [Space]
    [SerializeField]
    GameObject defaultEnemyPrefab;

    [SerializeField]
    List<SpawnInfo> spawnInfoList;

    float ElapsedTime => TimeRecordManager.Instance.TimeRecord + devStartTime;

    private void Start()
    {
        for (int i = spawnInfoList.Count - 1; i >= 0; i--)
        {
            SpawnInfo spawnInfo = spawnInfoList[i];
            if (devStartTime > spawnInfo.spawnTime)
            {
                spawnInfoList.RemoveAt(i);
            }
        }

    }

    private void Update()
    {
        for (int i = spawnInfoList.Count - 1; i >= 0; i--)
        {
            SpawnInfo spawnInfo = spawnInfoList[i];
            if (ElapsedTime > spawnInfo.spawnTime)
            {
                ObjectSpawner.Instance.SpawnObjects(spawnInfo.spawnPrefab, spawnInfo.spawnSide, spawnInfo.count, spawnInfo.spawnInterval);
                spawnInfoList.RemoveAt(i);
            }
        }
    }

    public void AddSpawnInfo()
    {
        float SpawnEndTime = GetSpawnEndTime();

        SpawnInfo info = new();
        info.spawnPrefab = defaultEnemyPrefab;
        info.spawnTime = GetSpawnEndTime();
        info.count = 1;
        info.spawnSide = Edge.Random;
        spawnInfoList.Add(info);
    }

    public float GetSpawnEndTime()
    {
        if (spawnInfoList.Count == 0) return 0;
        return spawnInfoList[spawnInfoList.Count - 1].SpawnEndTime;
    }
}

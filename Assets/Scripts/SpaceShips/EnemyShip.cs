﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable), typeof(BoundaryJump))]
public class EnemyShip : MonoBehaviour
{
    public int point = 100;
    public int upgradePoint = 0;

    [Multiline]
    public string desc;

    private void Start()
    {
        Damageable damageable = GetComponent<Damageable>();
        damageable.onDead.AddListener(delegate
        {
            UiManager.Instance.CreateText("+" + point, transform.position);
            ScoreManager.Instance.AddScore(point);
            LevelManager.Instance.GetExp(point);
            if(upgradePoint > 0) UpgradeManager.Instance.PointUp(upgradePoint);
        });        
    }

}

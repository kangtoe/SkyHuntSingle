using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class EnemyShip : MonoBehaviour
{
    public int point = 100;
    //public int spwanMinWave = 0; // 이 이후의 wave에만 등장 가능

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
        });
    }

}

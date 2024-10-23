using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일정 시간 경과시, 스택을 쌓는다
// 스택 사용 가능 여부를 판단
// 외부에서 스택을 사용할 수 있도록 기능 제공
// 스택을 사용하여 Shooter에서 사격을 가한다.

[RequireComponent(typeof(ShooterBase))]
public class MissleSystem : MonoBehaviour
{
    [SerializeField]
    ShooterBase missleShooter;

    [SerializeField]
    float stackDelay = 1f;
    float useStack = 0;

    [SerializeField]
    int maxStack = 5;
    int currStack = 0;

    public int MaxStack => maxStack;
    public int CurrStack => currStack;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMissleUI();
    }

    // Update is called once per frame
    void Update()
    {
        StackCheck();
    }

    public bool TryFire()
    {        
        if (currStack < 1) return false;

        bool isFired = missleShooter.TryFire();        
        if (isFired)
        {
            currStack--;
            UpdateMissleUI();
            useStack = Time.time;
        }

        return isFired;
    }

    void StackCheck()
    {
        if (currStack < MaxStack)
        {
            if (useStack + stackDelay < Time.time )
            {
                currStack++;
                useStack = Time.time;
                UpdateMissleUI();
            }
        }
    }

    void UpdateMissleUI()
    {
        CombatUiManager.Instance.SetMissleUI(CurrStack, MaxStack);
    }
}

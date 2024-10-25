using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 일정 시간 경과시, 스택을 쌓는다
// 스택 사용 가능 여부를 판단
// 외부에서 스택을 사용할 수 있도록 기능 제공
// 스택을 사용하여 Shooter에서 사격을 가한다.

[RequireComponent(typeof(ShooterBase))]
public class StackWeapon : MonoBehaviour
{
    [SerializeField]
    ShooterBase useShooter;

    [SerializeField]
    float chargeDelay = 1f;
    [SerializeField]
    float rechargeDelay = 1f; // hold charge stack when use
    float lastStackUsed = 0;

    [SerializeField]
    int maxStack = 5;
    float currStack = 0;

    [SerializeField]
    bool showRatio; // to show fill ratio?

    public int MaxStack => maxStack;
    public float CurrStack => showRatio ? currStack : Mathf.Floor(currStack);

    [HideInInspector]
    public UnityEvent onChangeValue = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        // wait recharge delay
        if (lastStackUsed != 0 && Time.time < lastStackUsed + rechargeDelay) return;

        if (currStack < MaxStack)
        {
            currStack += Time.deltaTime / chargeDelay;
            if(currStack > MaxStack) currStack = MaxStack;
        }
        
        onChangeValue.Invoke();        
    }

    public bool TryFire()
    {        
        if (currStack < 1) return false;

        bool isFired = useShooter.TryFire();        
        if (isFired)
        {
            currStack--;
            onChangeValue.Invoke();
            lastStackUsed = Time.time;
        }

        return isFired;
    }    
}

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
    float? currStack;

    [SerializeField]
    bool showRatio; // to show fill ratio?

    [SerializeField]
    bool fullStackStart = false;

    public int MaxStack => maxStack;
    public float CurrStack{
        get {
            if (currStack == null) InitStack();
            return currStack.Value;
        }
    }
    public float CurrStackOnUI => showRatio ? CurrStack : Mathf.Floor(CurrStack);

    [HideInInspector]
    public UnityEvent onChangeValue = new UnityEvent();

    private void Start()
    {
        onChangeValue.AddListener(delegate
        {
            currStack = Mathf.Clamp(CurrStack, 0, maxStack);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // wait recharge delay
        if (lastStackUsed != 0 && Time.time < lastStackUsed + rechargeDelay) return;
        
        if (currStack < MaxStack) currStack += Time.deltaTime / chargeDelay;                
        onChangeValue.Invoke();        
    }

    public bool TryFire(bool useForced = false)
    {        
        if (!useForced && currStack < 1) return false;

        bool isFired = useShooter.TryFire();        
        if (isFired)
        {
            currStack--;
            onChangeValue.Invoke();
            lastStackUsed = Time.time;
        }

        return isFired;
    }    

    public void InitStack()
    {
        if (fullStackStart) currStack = maxStack;
        else currStack = 0;
    }

    public void SetMaxStack(int amount)
    {
        maxStack = amount;
        onChangeValue.Invoke();
    }

    public void SetChargeDelay(float amount)
    {
        chargeDelay = amount;
    }

    public void SetDamage(int amount)
    {
        useShooter.SetDamage(amount);
    }
}

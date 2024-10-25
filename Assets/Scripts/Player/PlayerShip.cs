using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class PlayerShip : MonoBehaviour
{    
    [SerializeField] ShooterBase shooter;
    [SerializeField] HeatSystem heatSystem;
    [SerializeField] MoveStandard movement;
    [SerializeField] Damageable damageable;
    [SerializeField] StackWeapon missleSystem;
    [SerializeField] StackWeapon pulseSystem;

    float heatPerShot = 50;

    bool FireInput => InputManager.Instance.FireInput;
    bool MoveInput => InputManager.Instance.MoveInput;
    bool MissleInput => InputManager.Instance.MissleInput;
    bool PulseInput => InputManager.Instance.PulseInput;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();        

        damageable.onDamaged.AddListener(delegate
        {
            UpdateHealthUI();
        });

        missleSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetMissleUI(missleSystem.CurrStack, missleSystem.MaxStack);
        });

        pulseSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetpulseUI(pulseSystem.CurrStack, pulseSystem.MaxStack);
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FireCheck();
        MoveCheck();        
    }

    private void Update()
    {
        MissleCheck();
        PulseCheck();
    }

    void FireCheck()
    {
        if (heatSystem.OverHeated) return;

        if (FireInput)
        {
            bool fired = shooter.TryFire();
            if (fired)
            {
                heatSystem.AdjustHeat(heatPerShot);
                UiManager.Instance.AdjustCursorSpread(8);
            }
        }
    }

    void MoveCheck()
    {
        if(MoveInput)
        {
            movement.Move();
        }
    }

    void MissleCheck()
    {
        if (!MissleInput) return;   
        
        missleSystem.TryFire();        
    }

    void PulseCheck()
    {
        if (!PulseInput) return;

        pulseSystem.TryFire();
    }

    void UpdateHealthUI()
    {
        int curr = (int)damageable.CurrHealth;
        int max = (int)damageable.MaxHealth;
        UiManager.Instance.SetHealthUI(curr, max);
    }
}

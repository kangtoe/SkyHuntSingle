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

    bool FireInput => CombatInputManager.Instance.FireInput;
    bool MoveInput => CombatInputManager.Instance.MoveInput;
    bool MissleInput => CombatInputManager.Instance.MissleInput;
    bool PulseInput => CombatInputManager.Instance.PulseInput;

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
            CombatUiManager.Instance.SetMissleUI(missleSystem.CurrStack, missleSystem.MaxStack);
        });

        pulseSystem.onChangeValue.AddListener(delegate
        {
            CombatUiManager.Instance.SetpulseUI(pulseSystem.CurrStack, pulseSystem.MaxStack);
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
                CombatUiManager.Instance.AdjustCursorSpread(8);
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
        CombatUiManager.Instance.SetHealthUI(curr, max);
    }
}

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

    float heatPerShot = 50;

    bool FireInput => CombatInputManager.Instance.FireInput;
    bool MoveInput => CombatInputManager.Instance.MoveInput;
    bool MissleInput => CombatInputManager.Instance.MissleInput;

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

    void UpdateHealthUI()
    {
        int curr = (int)damageable.CurrHealth;
        int max = (int)damageable.MaxHealth;
        CombatUiManager.Instance.SetHealthUI(curr, max);
    }
}

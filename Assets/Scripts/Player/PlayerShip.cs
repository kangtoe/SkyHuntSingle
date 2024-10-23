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

    float heatPerShot = 50;

    public bool FireInput => CombatInputManager.Instance.FireInput;
    public bool MoveInput => CombatInputManager.Instance.MoveInput;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();

        damageable.onDamaged.AddListener(delegate
        {
            UpdateHealthUI();
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FireCheck();
        MoveCheck();
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

    void UpdateHealthUI()
    {
        int curr = (int)damageable.CurrHealth;
        int max = (int)damageable.MaxHealth;
        CombatUiManager.Instance.SetHealthUI(curr, max);
    }
}

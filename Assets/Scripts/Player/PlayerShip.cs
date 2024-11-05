using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class PlayerShip : MonoBehaviour
{    
    [Header("systems")]
    [SerializeField] ShooterBase shooter;
    [SerializeField] HeatSystem heatSystem;
    [SerializeField] MoveStandard movement;
    [SerializeField] Impactable impactable;
    [SerializeField] BrakeSystem breakSystem;
    [SerializeField] Damageable damageable;
    [SerializeField] StackWeapon missleSystem;
    [SerializeField] StackWeapon pulseSystem;

    [Header("amounts")]
    [SerializeField] float heatPerShot = 5;

    bool MoveForwardInput => InputManager.Instance.MoveForwardInput;
    Vector2 MoveDirectionInput => InputManager.Instance.MoveDirectionInput;
    bool BreakInput => InputManager.Instance.BrakeInput;

    bool FireInput => InputManager.Instance.FireInput;    
    bool MissleInput => InputManager.Instance.MissleInput;
    bool PulseInput => InputManager.Instance.PulseInput;

    bool isActiveMissleSystem;
    bool isActivePulseSystem;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();        

        damageable.onDamaged.AddListener(delegate
        {
            UpdateHealthUI();
            UiManager.Instance.ShakeUI();
        });

        missleSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetMissleUI(missleSystem.CurrStackOnUI, missleSystem.MaxStack);
        });

        pulseSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetpulseUI(pulseSystem.CurrStackOnUI, pulseSystem.MaxStack);
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        MoveCheck();        
    }

    private void Update()
    {
        if (!GameManager.Instance.OnPlay) return;

        BreakCheck();

        FireCheck();
        if(isActiveMissleSystem) UseMissle();
        if(isActivePulseSystem) UsePulse();
    }

    void MoveCheck()
    {
        if (MoveForwardInput)
        {
            movement.Move();
        }

        if(MoveDirectionInput != Vector2.zero)
        {
            movement.Move(MoveDirectionInput);
        }
    }

    void BreakCheck()
    {
        breakSystem.SetBreak(BreakInput);
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
            }
        }
    }

    public void ToggleMissleSystem(bool active, bool forceToggle = false)
    {
        if (!forceToggle && isActiveMissleSystem == active) return;

        isActiveMissleSystem = active;
        missleSystem.InitStack();
        UiManager.Instance.ToggleMissleUI(active);
    }

    public void TogglePulseSystem(bool active, bool forceToggle = false)
    {
        if (!forceToggle && isActivePulseSystem == active) return;

        isActivePulseSystem = active;
        pulseSystem.InitStack();
        UiManager.Instance.TogglePluseUI(active);
    }

    public void UseMissle(bool useForced = false)
    {
        if (!MissleInput) return;   
        
        missleSystem.TryFire(useForced);        
    }

    public void UsePulse(bool useForced = false)
    {
        if (!PulseInput) return;

        pulseSystem.TryFire(useForced);
    } 

    void UpdateHealthUI()
    {
        int curr = (int)damageable.CurrHealth;
        int max = (int)damageable.MaxHealth;
        UiManager.Instance.SetHealthUI(curr, max);
    }

    public void InitShip(bool emitPulse =  false)
    {
        pulseSystem.TryFire(emitPulse);
        pulseSystem.InitStack();
        missleSystem.InitStack();
        heatSystem.InitHeat();

        damageable.InitHealth();
        UpdateHealthUI();
    }

    public void SetSystem(UpgradeField _type, float amount)
    {
        switch (_type)
        {
            case UpgradeField.Shield:
                damageable.SetMaxHealth(amount * 100, true);
                UpdateHealthUI();
                break;
            case UpgradeField.Impact:
                impactable.SetDamageAmount(amount * 100);
                break;
            case UpgradeField.MultiShot:
                shooter.SetMultiShot((int)amount);
                break;
            case UpgradeField.Heat:
                heatPerShot = amount;
                break;
            case UpgradeField.Missle:
                missleSystem.SetMaxStack((int)amount);
                break;
            case UpgradeField.Reload:
                missleSystem.SetChargeDelay(amount);
                break;
            case UpgradeField.Power:
                pulseSystem.SetDamage((int)amount);
                break;
            case UpgradeField.Charge:
                pulseSystem.SetChargeDelay(amount);
                break;
        }
    }
}

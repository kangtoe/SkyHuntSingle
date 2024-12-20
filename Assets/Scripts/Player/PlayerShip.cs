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
    [SerializeField] BrakeSystem brakeSystem;
    [SerializeField] Damageable damageable;
    [SerializeField] StackWeapon missleSystem;
    [SerializeField] StackWeapon pulseSystem;

    [Header("amounts")]
    [SerializeField] float heatPerShot = 5;

    [Header("sounds")]
    [SerializeField] AudioClip failSound;

    bool MoveForwardInput => InputManager.Instance.MoveForwardInput;
    Vector2 MoveDirectionInput => InputManager.Instance.MoveDirectionInput;
    //bool BrakeInput => InputManager.Instance.BrakeInput;

    bool FireInput => InputManager.Instance.FireInput;    
    bool MissleInput => InputManager.Instance.MissleInput;
    bool PulseInput => InputManager.Instance.PulseInput;

    bool isActiveMissleSystem;
    bool isActivePulseSystem;

    bool canPrintOverHeat = true;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();        

        damageable.onDamaged.AddListener(delegate
        {
            UpdateHealthUI();
            UiManager.Instance.ShakeUI();
        });
        damageable.onDead.AddListener(delegate
        {
            GameManager.Instance.GameOver();
        });

        missleSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetMissleUI(missleSystem.CurrStackOnUI, missleSystem.MaxStack);
        });
        pulseSystem.onChangeValue.AddListener(delegate
        {
            UiManager.Instance.SetpulseUI(pulseSystem.CurrStackOnUI, pulseSystem.MaxStack);
        });

        LevelManager.Instance.onLevelUp.AddListener(delegate {
            UiManager.Instance.CreateText("Level Up!", transform.position);
        });
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        MoveCheck();        
    }

    private void Update()
    {
        bool onCombat = GameManager.Instance.GameState == GameState.OnCombat;
        bool onTitle = GameManager.Instance.GameState == GameState.OnTitle;

        if (!onCombat && !onTitle) return;

        //BreakCheck();
        FireCheck();
        UseMissle();

        if (onTitle) return;
        UsePulse();
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

    //void BreakCheck()
    //{
    //    brakeSystem.SetBreak(BrakeInput);
    //}

    void FireCheck()
    {
        if (!FireInput)
        {
            canPrintOverHeat = true;
            return;
        } 

        if (heatSystem.OverHeated)
        {
            if (canPrintOverHeat)
            {
                UiManager.Instance.CreateText("OverHeat!", transform.position);
                SoundManager.Instance.PlaySound(failSound);
            } 

            canPrintOverHeat = false;
            return;
        }
        else
        {
            canPrintOverHeat = true;
        }
        
        bool fired = shooter.TryFire();
        if (fired)
        {
            heatSystem.AdjustHeat(heatPerShot);
        }
    }
    
    public void UseMissle(bool withoutCunsume = false)
    {
        if (!MissleInput) return;

        if (withoutCunsume || isActiveMissleSystem)
        {
            bool succeed = missleSystem.UseStack(withoutCunsume);
            if (succeed) return; 
        }

        UiManager.Instance.CreateText("no missle!", transform.position);
        SoundManager.Instance.PlaySound(failSound);
    }

    public void UsePulse(bool withoutCunsume = false)
    {
        if (!PulseInput) return;

        if (withoutCunsume || isActivePulseSystem)
        {
            bool succeed = pulseSystem.UseStack(withoutCunsume);
            if (succeed) return;
        }

        UiManager.Instance.CreateText("no pulse!", transform.position);
        SoundManager.Instance.PlaySound(failSound);
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

    void UpdateHealthUI()
    {
        int curr = (int)damageable.CurrHealth;
        int max = (int)damageable.MaxHealth;
        UiManager.Instance.SetHealthUI(curr, max);
    }

    public void InitShip(bool stackFull = false)
    {        
        if(stackFull) UiManager.Instance.CreateText("Restore All!", transform.position);

        pulseSystem.InitStack(stackFull);
        missleSystem.InitStack(stackFull);
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
            case UpgradeField.OnImpact:
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
            case UpgradeField.ReloadTime:
                missleSystem.SetChargeDelay(amount);
                break;
            //case UpgradeField.Damage:
                //pulseSystem.SetDamage((int)amount);
                //break;
            case UpgradeField.ChargeTime:
                pulseSystem.SetChargeDelay(amount);
                break;
        }
    }
}

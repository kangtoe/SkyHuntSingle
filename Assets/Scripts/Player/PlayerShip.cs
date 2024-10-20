using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{    
    [SerializeField] ShooterBase shooter;
    [SerializeField] HeatSystem heatSystem;
    [SerializeField] MoveStandrad movement;

    float heatPerShot = 50;

    public bool FireInput => CombatInputManager.Instance.FireInput;
    public bool MoveInput => CombatInputManager.Instance.MoveInput;

    // Start is called before the first frame update
    void Start()
    {
        
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
                CombatUiManager.Instance.Crosshair.AdjustSpread(8);
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
}

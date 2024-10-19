using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] ShooterBase shooter;
    [SerializeField] HeatSystem heatSystem;

    float heatPerShot = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (heatSystem.OverHeated) return;  

        if(CombatInputManager.Instance.FireInput)
        {
            bool fired = shooter.TryFire();
            if (fired)
            {
                heatSystem.AdjustHeat(heatPerShot);
                CombatUiManager.Instance.Crosshair.AdjustSpread(8);
            }
        }
    }    
}

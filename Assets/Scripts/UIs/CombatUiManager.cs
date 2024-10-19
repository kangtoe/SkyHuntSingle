using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUiManager : MonoSingleton<CombatUiManager>
{ 
    Crosshair Crosshair 
    {
        get {
            if (!crosshair) crosshair = FindObjectOfType<Crosshair>();
            return crosshair;
        }
    }
    Crosshair crosshair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

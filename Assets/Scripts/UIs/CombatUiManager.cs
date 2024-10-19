using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUiManager : MonoSingleton<CombatUiManager>
{ 
    public Crosshair Crosshair 
    {
        get {
            if (!crosshair) crosshair = FindObjectOfType<Crosshair>();
            return crosshair;
        }
    }
    Crosshair crosshair;

    [SerializeField]
    Text heatText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeatText(float percent)
    {
        heatText.text =  Mathf.Floor(percent) + "%";
    }
}

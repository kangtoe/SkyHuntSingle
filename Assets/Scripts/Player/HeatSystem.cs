using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSystem : MonoBehaviour
{
    float heatMax = 100;    
    float heatCool = 20;
    float currHeat = 0;

    public bool OverHeated => overHeated;
    bool overHeated = false;

    float coolWait = 0;

    // Update is called once per frame
    void Update()
    {
        if(coolWait > 0)
        {
            coolWait -= Time.deltaTime;
            return;
        }
        else
        {
            AdjustHeat(heatCool * -1 * Time.deltaTime);
        }        

        if (OverHeated)
        {
            if (currHeat <= 0) overHeated = false;
            else return;
        }
    }

    public void AdjustHeat(float f)
    {
        currHeat += f;
        currHeat = Mathf.Clamp(currHeat, 0, heatMax);

        if (f > 0) coolWait = 0.5f;

        if (currHeat == heatMax) overHeated = true;

        float ratio = currHeat / heatMax;
        UiManager.Instance.SetHeatText(ratio);
        UiManager.Instance.SetCursorSpread(ratio);
    }

    public void InitHeat()
    {
        currHeat = 0;
        coolWait = 0;
    }
}

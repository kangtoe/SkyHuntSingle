using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] UpgradeButtonUI ShipBtn;
    [SerializeField] UpgradeButtonUI ShooterBtn;
    [SerializeField] UpgradeButtonUI MissleBtn;
    [SerializeField] UpgradeButtonUI PulseBtn;
    [SerializeField] UpgradeButtonUI SuperchargeBtn;

    public Dictionary<UpgradeType, int> upgradeState = new()
    {
        { UpgradeType.Ship, 1 },
        { UpgradeType.Shooter, 2 },
        { UpgradeType.Missle, 3 },
        { UpgradeType.Pulse, 0 },
    };

    int upgradePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitButtonUIs();
    }

    void UsePoint(UpgradeType _type)
    {
        if (upgradePoint < 1) return;
        if (upgradeState[_type] >= UpgradeData.MaxLevel) return;

        upgradePoint--;

        if(_type != UpgradeType.Supercharge) upgradeState[_type]++;

        switch (_type)
        {
            case UpgradeType.Ship:
                break;
            case UpgradeType.Shooter:
                break;
            case UpgradeType.Missle:
                break;
            case UpgradeType.Pulse:
                break;
            case UpgradeType.Supercharge:
                break;
        }

        InitButtonUIs();
    }

    void InitButtonUIs()
    {        
        UpdateUpgradeButton(ShipBtn, UpgradeType.Ship);
        UpdateUpgradeButton(ShooterBtn, UpgradeType.Shooter);
        UpdateUpgradeButton(MissleBtn, UpgradeType.Missle);
        UpdateUpgradeButton(PulseBtn, UpgradeType.Pulse);
        UpdateUpgradeButton(SuperchargeBtn, UpgradeType.Supercharge);
    }

    void UpdateUpgradeButton(UpgradeButtonUI btn, UpgradeType _type)
    {
        //List<UpgradeFieldInfo> infos = new();
        //infos.Add(new UpgradeFieldInfo("shield", 3, 3));
        //infos.Add(new UpgradeFieldInfo("impact", 0.5f, 0.25f));

        List<UpgradeFieldInfo> infos = null;
        int level = 0;

        if (_type != UpgradeType.Supercharge)
        {
            infos = GetInfos(_type);
            level = upgradeState[_type];
        } 
        
        btn.UpdateTitle(_type, level);
        btn.UpdateDesc(_type, infos);
    }

    List<UpgradeFieldInfo> GetInfos(UpgradeType _type)
    {
        List<UpgradeFieldInfo> infos = new();
        int level = upgradeState[_type];

        switch (_type)
        {
            case UpgradeType.Ship:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Shield, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Impact, level));
                break;

            case UpgradeType.Shooter:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.MultiShot, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Heat, level));
                break;

            case UpgradeType.Missle:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Missle, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Reload, level));
                break;

            case UpgradeType.Pulse:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Power, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeFieldType.Charge, level));
                break;
        }

        return infos;
    }
}

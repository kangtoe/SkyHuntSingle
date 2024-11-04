using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] Color textHighlight;

    UpgradeButtonUI ShipBtn => UiManager.instance.ShipBtn;
    UpgradeButtonUI ShooterBtn => UiManager.instance.ShooterBtn;
    UpgradeButtonUI MissleBtn => UiManager.instance.MissleBtn;
    UpgradeButtonUI PulseBtn => UiManager.instance.PulseBtn;
    UpgradeButtonUI SuperchargeBtn => UiManager.instance.SuperchargeBtn;

    Dictionary<UpgradeType, int> upgradeState = new()
    {
        { UpgradeType.Ship, 1 },
        { UpgradeType.Shooter, 2 },
        { UpgradeType.Missle, 3 },
        { UpgradeType.Pulse, 0 },
    };    

    int upgradePoint = 10;

    // Start is called before the first frame update
    void Start()
    {
        InitButtonUIs();
        UiManager.instance.SetUpgradePointText(upgradePoint);

        ShipBtn.Button.onClick.AddListener(delegate { 
            TryUsePoint(UpgradeType.Ship); 
        });
        ShooterBtn.Button.onClick.AddListener(delegate { 
            TryUsePoint(UpgradeType.Shooter); 
        });
        MissleBtn.Button.onClick.AddListener(delegate { 
            TryUsePoint(UpgradeType.Missle); 
        });
        PulseBtn.Button.onClick.AddListener(delegate { 
            TryUsePoint(UpgradeType.Pulse); 
        });
        SuperchargeBtn.Button.onClick.AddListener(delegate { 
            TryUsePoint(UpgradeType.Supercharge); 
        });
    }

    bool TryUsePoint(UpgradeType _type)
    {
        if (upgradePoint < 1) return false;
        if (upgradeState[_type] >= UpgradeData.MaxLevel) return false;

        upgradePoint--;
        UiManager.instance.SetUpgradePointText(upgradePoint);

        // add amount
        if (_type != UpgradeType.Supercharge) upgradeState[_type]++;

        // apply amount
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
        return true;
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
        
        string title = UpgradeData.GetTitleString(_type, level);
        Debug.Log(title);
        btn.SetTitle(title);
        
        string colorCode = ColorUtility.ToHtmlStringRGBA(textHighlight);
        string desc = UpgradeData.GetDescString(_type, level, infos, colorCode);
        btn.SetDesc(desc);
    }

    List<UpgradeFieldInfo> GetInfos(UpgradeType _type)
    {
        List<UpgradeFieldInfo> infos = new();
        int level = upgradeState[_type];

        switch (_type)
        {
            case UpgradeType.Ship:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Shield, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Impact, level));
                break;

            case UpgradeType.Shooter:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.MultiShot, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Heat, level));
                break;

            case UpgradeType.Missle:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Missle, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Reload, level));
                break;

            case UpgradeType.Pulse:
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Power, level));
                infos.Add(UpgradeData.GetFieldInfo(UpgradeField.Charge, level));
                break;
        }

        return infos;
    }
}

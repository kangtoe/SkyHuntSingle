using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [SerializeField] AudioClip upgradeSound;
    [SerializeField] AudioClip upgradeFailSound;

    [SerializeField] Color textHighlight;

    UpgradeButtonUI ShipBtn => UiManager.Instance.ShipBtn;
    UpgradeButtonUI ShooterBtn => UiManager.Instance.ShooterBtn;
    UpgradeButtonUI MissleBtn => UiManager.Instance.MissleBtn;
    UpgradeButtonUI PulseBtn => UiManager.Instance.PulseBtn;
    UpgradeButtonUI SuperchargeBtn => UiManager.Instance.SuperchargeBtn;

    Dictionary<UpgradeType, int> upgradeState = new()
    {
        { UpgradeType.Ship, 1 },
        { UpgradeType.Shooter, 1 },
        { UpgradeType.Missle, 1 },
        { UpgradeType.Pulse, 1 },
    };    

    int upgradePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitButtonUIs();
        InitPlayershipSystems(true);
        UiManager.Instance.SetUpgradePointText(upgradePoint);

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
        if (upgradePoint < 1)
        {
            UiManager.Instance.CreateText("No Point!", true);
            UiManager.Instance.ShakeUI();
            SoundManager.Instance.PlaySound(upgradeFailSound);
            return false;
        }

        if (_type == UpgradeType.Supercharge)
        {
            GameManager.Instance.PlayerShip.InitShip(true);
            GameManager.Instance.ToggleUpgradeState(false);
        }
        else
        {
            if (upgradeState[_type] >= UpgradeData.MaxLevel)
            {
                UiManager.Instance.CreateText("Max Level!", true);
                UiManager.Instance.ShakeUI();
                SoundManager.Instance.PlaySound(upgradeFailSound);
                return false;
            }            

            // add amount
            upgradeState[_type]++;

            // apply amount
            InitPlayershipSystems();
            InitButtonUIs();
        }

        upgradePoint--;
        UiManager.Instance.SetUpgradePointText(upgradePoint);
        
        SoundManager.Instance.PlaySound(upgradeSound);
        return true;
    }

    void InitPlayershipSystems(bool forceToggle = false)
    {
        var player = GameManager.Instance.PlayerShip;
        foreach (var (upgradeType, level) in upgradeState)
        {
            if (upgradeType == UpgradeType.Missle)
            {
                if (level == 0)
                {
                    player.ToggleMissleSystem(false, forceToggle);
                    continue;
                }
                else player.ToggleMissleSystem(true);
                
            }
            if (upgradeType == UpgradeType.Pulse)
            {
                if (level == 0)
                {
                    player.TogglePulseSystem(false, forceToggle);
                    continue;
                } 
                else player.TogglePulseSystem(true);                
            }

            UpgradeField[] fields = UpgradeData.GetRalatedFields(upgradeType);
            
            foreach (UpgradeField field in fields)
            {                
                var info = UpgradeData.GetFieldInfo(field, level);
                player.SetSystem(field, info.currAmount);
            }
        }

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
        //Debug.Log(title);
        btn.SetTitle(title);
        
        string colorCode = ColorUtility.ToHtmlStringRGBA(textHighlight);
        string desc = UpgradeData.GetDescString(_type, level, infos, colorCode);
        btn.SetDesc(desc);
    }

    List<UpgradeFieldInfo> GetInfos(UpgradeType _type)
    {
        List<UpgradeFieldInfo> infos = new();
        int level = upgradeState[_type];

        UpgradeField[] fields = UpgradeData.GetRalatedFields(_type);
        foreach (UpgradeField field in fields)
        {
            infos.Add(UpgradeData.GetFieldInfo(field, level));
        }

        return infos;
    }

    public void PointUp(int amount = 1)
    {
        upgradePoint += amount;
        UiManager.Instance.SetUpgradePointText(upgradePoint);
        GameManager.Instance.ToggleUpgradeState(true);
    }
}

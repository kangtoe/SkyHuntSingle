using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFieldInfo
{
    public string infoText;
    public float currAmount;
    public float nextAmount;

    public UpgradeFieldInfo(string infoText, float currAmount, float nextAmount)
    {
        this.infoText = infoText;
        this.currAmount = currAmount;
        this.nextAmount = nextAmount;
    }
}

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] Color textHighlight;

    [SerializeField] Text title;
    [SerializeField] Text desc;

    [Header("info")]
    [SerializeField] UpgradeType type;

    private void OnValidate()
    {
        UpdateTitle(type);

        List<UpgradeFieldInfo> fieldInfos = new List<UpgradeFieldInfo>();
        fieldInfos.Add(new UpgradeFieldInfo("shield", 3, 3));
        fieldInfos.Add(new UpgradeFieldInfo("impact", 0.5f, 0.25f));
        UpdateDesc(type, fieldInfos);
    }

    public void UpdateDesc(UpgradeType _type, List<UpgradeFieldInfo> fieldInfos)
    {
        string colorCode = ColorUtility.ToHtmlStringRGBA(textHighlight);

        string str = "";

        if (_type == UpgradeType.Supercharge)
        {
            
            str += "- EFFECT -";
            str += $"\n<color=#{colorCode}>restore</color> shield";
            str += $"\n<color=#{colorCode}>initialize</color> heat";
            str += $"\n<color=#{colorCode}>reload</color> missles";
            str += $"\n<color=#{colorCode}>emit</color> pulse";
                   
        }
        else
        {
            str += "- UPGRADE -";
            foreach (UpgradeFieldInfo fieldInfo in fieldInfos)
            {
                str += "\n" + fieldInfo.infoText + " " + fieldInfo.currAmount + " -> " + $"<color=#{colorCode}>" + fieldInfo.nextAmount + "</color>";
            }
        }
        
        desc.text = str;
    }

    public void UpdateTitle(UpgradeType _type, int level = 0)
    {        
        if(_type == UpgradeType.Supercharge)
        {
            title.text = UpgradeType.Supercharge.ToString();
        }
        else
        {
            string levelText = GetRomanNumber(level);

            title.text = _type.ToString() + " Lv." + levelText;
        }
    }

    string GetRomanNumber(int level)
    {        
        switch (level)
        {            
            case 1:
                return "I";        
            case 2:
                return "II";
            case 3:
                return "III";     
            case 4:
                return "IV";      
            case 5:
                return "V";
            default:
                return "-";
        }                        
    }
}

using System.Collections;
using System.Collections.Generic;

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

public static class UpgradeData
{
    public static int MaxLevel = 5;

    static Dictionary<UpgradeField, float[]> Datas = new()
    {
        { UpgradeField.Shield,      new float[6] { 1,       2,       2,      3,      3,      4       } },
        { UpgradeField.Impact,      new float[6] { 1,       1,       0.5f,   0.5f,   0.25f,  0.25f   } },

        { UpgradeField.MultiShot,   new float[6] { 0,       0,       0,      0,      0,      0       } },
        { UpgradeField.Heat,        new float[6] { 0,       0,       0,      0,      0,      0       } },

        { UpgradeField.Missle,      new float[6] { 0,       0,       0,      0,      0,      0       } },
        { UpgradeField.Reload,      new float[6] { 0,       0,       0,      0,      0,      0       } },

        { UpgradeField.Power,       new float[6] { 0,       0,       0,      0,      0,      0       } },
        { UpgradeField.Charge,      new float[6] { 0,       0,       0,      0,      0,      0       } },
    };

    static T GetSafeElem<T>(T[] arr, int idx)
    {
        if (idx < 0) return arr[0];
        if (idx >= arr.Length) return arr[arr.Length -1];
        return arr[idx];
    }

    static (float, float) GetAmount(UpgradeField _type, int level)
    {
        float[] arr = Datas[_type];

        float curr = GetSafeElem(arr, level);
        float next = GetSafeElem(arr, level + 1);

        return (curr, next);
    }

    public static UpgradeFieldInfo GetFieldInfo(UpgradeField _type, int level)
    {
        UpgradeFieldInfo info;

        var (curr, next) = GetAmount(_type, level);
        info = new UpgradeFieldInfo(_type.ToString(), curr, next);

        return info;
    }

    public static string GetDescString(UpgradeType _type, int level, List<UpgradeFieldInfo> fieldInfos, string colorCode)
    {
        string str = "";
        bool isMaxlevel = level >= MaxLevel;

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
                str += "\n" + fieldInfo.infoText + " " + fieldInfo.currAmount;
                if(!isMaxlevel) str += " -> " + $"<color=#{colorCode}>" + fieldInfo.nextAmount + "</color>";
            }
            if (isMaxlevel) str += "\n" + "Max Level";
        }

        return str;
    }

    public static string GetTitleString(UpgradeType _type, int level = 0)
    {
        string str;

        if (_type == UpgradeType.Supercharge)
        {
            str = UpgradeType.Supercharge.ToString();
        }
        else
        {
            string levelText = GetRomanNumber(level);
            str = _type.ToString() + " Lv." + levelText;
        }

        return str;
    }

    static string GetRomanNumber(int level)
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

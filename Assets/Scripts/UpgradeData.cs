using System.Collections;
using System.Collections.Generic;

public enum UpgradeFieldType
{
    Shield,
    Impact,
    MultiShot,
    Heat,
    Missle,
    Reload,
    Power,
    Charge
}

public static class UpgradeData
{
    public static int MaxLevel = 5;

    static Dictionary<UpgradeFieldType, float[]> Datas = new()
    {
        { UpgradeFieldType.Shield,      new float[5] { 2,       2,      3,      3,      4       } },
        { UpgradeFieldType.Impact,      new float[5] { 1,       0.5f,   0.5f,   0.25f,  0.25f   } },

        { UpgradeFieldType.MultiShot,   new float[5] { 0,       0,      0,      0,      0       } },
        { UpgradeFieldType.Heat,        new float[5] { 0,       0,      0,      0,      0       } },

        { UpgradeFieldType.Missle,      new float[5] { 0,       0,      0,      0,      0       } },
        { UpgradeFieldType.Reload,      new float[5] { 0,       0,      0,      0,      0       } },

        { UpgradeFieldType.Power,       new float[5] { 0,       0,      0,      0,      0       } },
        { UpgradeFieldType.Charge,      new float[5] { 0,       0,      0,      0,      0       } },
    };

    static (float, float) GetAmount(UpgradeFieldType _type, int level)
    {
        float[] arr = Datas[_type];

        float curr = arr[level];
        float next = level >= arr.Length ? arr[arr.Length] : arr[level + 1];

        return (curr, next);
    }

    public static UpgradeFieldInfo GetFieldInfo(UpgradeFieldType _type, int level)
    {
        UpgradeFieldInfo info;

        var (curr, next) = GetAmount(_type, level);
        info = new UpgradeFieldInfo(_type.ToString(), curr, next);

        return info;
    }
}

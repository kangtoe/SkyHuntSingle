using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] AudioClip levelUpSound;

    int level = 1;
    int exp = 0;

    int NextLevelExp => level * 1100;

    private void Start()
    {
        UpdateExpUI();
    }

    public void GetExp(int Amount)
    {
        exp += Amount;
        while (exp >= NextLevelExp)
        {            
            exp -= NextLevelExp;
            LevelUp();
        }

        UpdateExpUI();
    }

    void UpdateExpUI()
    {
        UiManager.Instance.SetLevelText(level);

        float ratio = (float)exp / NextLevelExp;
        UiManager.Instance.SetExpGage(ratio);
    }

    void LevelUp()
    {
        level++;        
        SoundManager.Instance.PlaySound(levelUpSound);
    }
}
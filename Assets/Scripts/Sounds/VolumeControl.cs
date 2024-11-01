using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// 볼륨 제어
public class VolumeControl : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    float mute_dB = -80;
    [SerializeField]
    float min_dB = -40;
    [SerializeField]
    float max_dB = 0;

    [SerializeField]
    float currentVolume_dB_bgm;
    [SerializeField]
    float currentVolume_dB_sfx;

    [Header("volumes")]
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    [Range(0, 1)]
    [SerializeField]
    float bgmVolume;
    public float BgmVolume
    {
        get
        {
            return bgmVolume;
        }
        set
        {
            bgmVolume = value;
            UpdateMixer();
        }
    }

    [Range(0, 1)]
    [SerializeField]
    float sfxVolume;
    public float SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;            
            UpdateMixer();
        }
    }

    private void Start()
    {        
        BgmVolume = 0;// SaveManager.BgmVolume;
        SfxVolume = 0;// SaveManager.SfxVolume;        

        InitSliders();
        UpdateMixer();
    }

    private void OnValidate()
    {
        UpdateMixer();
    }

    void InitSliders()
    {
        bgmSlider.normalizedValue = BgmVolume;
        bgmSlider.onValueChanged.AddListener(delegate {
            BgmVolume = bgmSlider.normalizedValue;
        });

        sfxSlider.normalizedValue = SfxVolume;
        sfxSlider.onValueChanged.AddListener(delegate {
            SfxVolume = sfxSlider.normalizedValue;
        });

    }

    // 실제 볼륨 제어
    void UpdateMixer()
    {
        currentVolume_dB_bgm = VolumeToDecibel(bgmVolume);
        audioMixer.SetFloat("BGM", currentVolume_dB_bgm);        

        currentVolume_dB_sfx = VolumeToDecibel(sfxVolume);
        audioMixer.SetFloat("SFX", currentVolume_dB_sfx);
    }

    float VolumeToDecibel(float volume)
    {
        float dB;
        if (volume == 0) dB = mute_dB;
        else dB = Mathf.Lerp(min_dB, max_dB, volume);

        return dB;
    }
}

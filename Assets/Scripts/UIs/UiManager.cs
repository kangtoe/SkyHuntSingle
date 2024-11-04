using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [Header("combat ui")]
    [SerializeField] Crosshair crosshair;
    [SerializeField] Text heatText;
    [SerializeField] Text scoreText;
    [SerializeField] MultyUiCtrl healthUI;
    [SerializeField] MultyUiCtrl missleUI;
    [SerializeField] MultyUiCtrl pulseUI;
    [SerializeField] Text levelText;
    [SerializeField] Image expGage;

    [Header("upgrade ui")]
    [SerializeField] UpgradeButtonUI shipBtn;
    [SerializeField] UpgradeButtonUI shooterBtn;
    [SerializeField] UpgradeButtonUI missleBtn;
    [SerializeField] UpgradeButtonUI pulseBtn;
    [SerializeField] UpgradeButtonUI superchargeBtn;
    public UpgradeButtonUI ShipBtn => shipBtn;
    public UpgradeButtonUI ShooterBtn => shooterBtn;
    public UpgradeButtonUI MissleBtn => missleBtn;
    public UpgradeButtonUI PulseBtn => pulseBtn;
    public UpgradeButtonUI SuperchargeBtn => superchargeBtn;
    [SerializeField] Text upgradePointText;

    [Header("toggles")]
    [SerializeField] Text helpText;
    [SerializeField] Image upgradePanel;
    [SerializeField] Image settingsPanel;

    [Header("canvas")]
    [SerializeField] Canvas titleCanvas;
    [SerializeField] Canvas cambatCanvas;

    [Header("volumes")]
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    public Slider BgmSlider => bgmSlider;
    public Slider SfxSlider => sfxSlider;


    public bool OnHelp => onHelp;
    bool onHelp;

    public bool OnUpgrade => onUpgrade;
    bool onUpgrade;

    public bool OnSettings => onSettings;
    bool onSettings;

    private void Start()
    {
        onHelp = helpText.gameObject.activeSelf;
        onUpgrade = upgradePanel.gameObject.activeSelf;
        onSettings = settingsPanel.gameObject.activeSelf;
    }

    public void SetCursorSpread(float ratio)
    {
        crosshair.SetArrSpreadRatio(ratio);
    }

    public void SetHeatText(float percent)
    {
        heatText.text =  Mathf.Floor(percent) + "%";
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "SCORE : " + score.ToString("000,000");
    }

    public void SetHealthUI(int currHealth, int maxHealth)
    {
        healthUI.InitUI(maxHealth / 100, (float)currHealth / 100);        
    }

    public void SetMissleUI(float currMissle, int maxMissle)
    {
        missleUI.InitUI(maxMissle, currMissle);
    }

    public void SetpulseUI(float currPulse, int maxPulse)
    {
        //Debug.Log(currPulse + " " + maxPulse);

        pulseUI.InitUI(maxPulse, currPulse);
    }

    public void SetCanvas(GameState state)
    {
        switch (state)
        {
            case GameState.OnTitle:
                titleCanvas.gameObject.SetActive(true);
                cambatCanvas.gameObject.SetActive(false);
                break;
            case GameState.OnCombat:
                titleCanvas.gameObject.SetActive(false);
                cambatCanvas.gameObject.SetActive(true);
                break;
        }        
    }

    public void ToggleHelpUI(bool active)
    {
        helpText.gameObject.SetActive(active);
        onHelp = active;
    }

    public void ToggleUpgradeUI(bool active)
    {
        //Debug.Log("ToggleUpgradeUI " + active);

        upgradePanel.gameObject.SetActive(active);
        onUpgrade = active;
    }

    public void ToggleSettingsUI(bool active)
    {
        settingsPanel.gameObject.SetActive(active);
        onSettings = active;
    }

    public void SetLevelText(int level)
    {
        levelText.text = "LV." + level.ToString("D2");
    }

    public void SetExpGage(float ratio)
    {
        expGage.fillAmount = ratio;
    }

    public void SetUpgradePointText(int point)
    {
        upgradePointText.text = "point : " + point.ToString("D2");
    }
}

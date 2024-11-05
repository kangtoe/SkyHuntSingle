using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] RectTransform root;

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

    [Header("Panels")]
    [SerializeField] Text helpText;
    [SerializeField] Image upgradePanel;
    [SerializeField] Image settingsPanel;
    [SerializeField] RectTransform titlePanel;
    [SerializeField] RectTransform combatPanel;   

    [Header("volumes")]
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    public Slider BgmSlider => bgmSlider;
    public Slider SfxSlider => sfxSlider;

    [Header("prefab")]
    [SerializeField] GameObject floatingText;

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

    public void SetHeatText(float ratio)
    {
        heatText.text =  Mathf.Floor(ratio * 100) + "%";
        heatText.color = Color.Lerp(Color.white, Color.red, ratio);
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

    public void ToggleMissleUI(bool active)
    {
        missleUI.gameObject.SetActive(active);
    }

    public void TogglePluseUI(bool active)
    {
        pulseUI.gameObject.SetActive(active);
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
                titlePanel.gameObject.SetActive(true);
                combatPanel.gameObject.SetActive(false);
                break;
            case GameState.OnCombat:
                titlePanel.gameObject.SetActive(false);
                combatPanel.gameObject.SetActive(true);
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

    public void ToggleCustomCursor(bool active)
    {
        Cursor.visible = !active;
        crosshair.gameObject.SetActive(active);
    }

    public void CreateText(string str, bool onMousePos = false)
    {
        Text txt = Instantiate(floatingText, root).GetComponent<Text>();
        txt.text = str;

        if (onMousePos)
        {
            txt.rectTransform.position = Input.mousePosition;
        }
    }

    public void ShakeUI(float _amount = 10f, float _duration = 0.2f)
    {
        IEnumerator ShakeCr(float _amount, float _duration)
        {
            float timer = 0;
            while (timer <= _duration)
            {
                root.anchoredPosition = (Vector3)Random.insideUnitCircle * _amount;

                timer += Time.unscaledDeltaTime;
                //yield return new WaitForSeconds(0.1f);
                yield return null;
            }
            root.anchoredPosition = Vector2.zero;
        }

        StopAllCoroutines();
        StartCoroutine(ShakeCr(_amount, _duration));
    }
}

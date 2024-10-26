using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{     
    [SerializeField]
    Crosshair crosshair;

    [SerializeField]
    Text heatText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    MultyUiCtrl healthUI;

    [SerializeField]
    MultyUiCtrl missleUI;

    [SerializeField]
    MultyUiCtrl pulseUI;

    [Header("canvas")]
    [SerializeField] Canvas titleCanvas;
    [SerializeField] Canvas cambatCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustCursorSpread(float f)
    {
        crosshair.AdjustSpread(f);
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
        Debug.Log(currPulse + " " + maxPulse);

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
            case GameState.StartCombat:
                titleCanvas.gameObject.SetActive(false);
                cambatCanvas.gameObject.SetActive(true);
                break;
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip overSound;
    [SerializeField] AudioClip clearSound;

    [Header("for debug")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerShip playerShip;

    public GameState GameState => gameState;
    public PlayerShip PlayerShip
    {
        get {
            if (!playerShip) playerShip = FindObjectOfType<PlayerShip>();
            return playerShip;
        }
    }

    GameState beforeState;    

    private void Awake()
    {
        //playerShip.enabled = false;        

        gameState = GameState.OnTitle;
        UiManager.Instance.SetCanvas(GameState.OnTitle);
        Time.timeScale = 1;

        UiManager.Instance.ToggleCustomCursor(true);
    }

    // Update is called once per frame
    void Update()
    {        
        if (InputManager.Instance.PulseInput)
        {
            if (gameState == GameState.OnTitle)
            {                
                GameStart(0.1f);
            }            
        }

        if (InputManager.Instance.HelpInput)
        {
            UiManager.Instance.ToggleHelpUI(!UiManager.Instance.OnHelp);
        } 

        if (InputManager.Instance.UpgradeInput)
        {
            if (gameState == GameState.OnUpgrade)
            {
                ToggleUpgradeState(false);
            }
            else if (gameState == GameState.OnCombat)
            {
                ToggleUpgradeState(true);
            }                                         
        }

        if (InputManager.Instance.EscapeInput)
        {
            if (gameState == GameState.OnUpgrade)
            {
                ToggleUpgradeState(false);
            }
            else
            {
                if (gameState == GameState.OnPaused)
                {
                    UiManager.Instance.ToggleSettingsUI(false);
                    UiManager.Instance.ToggleCustomCursor(true);
                    Time.timeScale = 1;
                    gameState = beforeState;                    
                }
                else
                {
                    UiManager.Instance.ToggleSettingsUI(true);
                    UiManager.Instance.ToggleCustomCursor(false);
                    Time.timeScale = 0;
                    beforeState = gameState;
                    gameState = GameState.OnPaused;                    
                }
                
            }
        }

        if (InputManager.Instance.RInput)
        {
            if(gameState == GameState.OnPaused || gameState == GameState.GameOver) RestartGame();
        }
    }

    public void ToggleUpgradeState(bool active)
    {
        UiManager.Instance.ToggleUpgradeUI(active);
        UiManager.Instance.ToggleCustomCursor(!active);
        Time.timeScale = active ? 0 : 1;
        gameState = active ?  GameState.OnUpgrade : GameState.OnCombat;
    }

    void GameStart(float delay)
    {
        IEnumerator GameStartCr(float delay)
        {
            playerShip.InitShip(true);

            yield return new WaitForSeconds(delay);

            gameState = GameState.OnCombat;
            UiManager.Instance.SetCanvas(GameState.OnCombat);
            SoundManager.Instance.PlaySound(startSound);
            TimeRecordManager.Instance.SetActiveCount(true);
        }

        if (gameState != GameState.OnTitle) return;        
        StartCoroutine(GameStartCr(delay));
    }    

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        UiManager.Instance.SetCanvas(GameState.GameOver);
        SoundManager.Instance.PlaySound(overSound);
        TimeRecordManager.Instance.SetActiveCount(false);
    }

}

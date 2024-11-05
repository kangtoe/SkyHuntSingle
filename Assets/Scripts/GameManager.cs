using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{    
    [Header("for debug")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerShip playerShip;
    public PlayerShip PlayerShip
    {
        get {
            if (!playerShip) playerShip = FindObjectOfType<PlayerShip>();
            return playerShip;
        }
    }

    [Header("intro effect")]
    [SerializeField] GameObject introEffect;
    [SerializeField] AudioClip introSound;

    GameState beforeState;

    public bool OnPlay => gameState == GameState.OnCombat || gameState == GameState.OnTitle;

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
                playerShip.InitShip();
                
                Instantiate(introEffect, playerShip.transform.position, Quaternion.identity);
                SoundManager.Instance.PlaySound(introSound);

                StartCoroutine(GameStartCr(0.1f));
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
                UiManager.Instance.ToggleUpgradeUI(false);
                UiManager.Instance.ToggleCustomCursor(true);
                Time.timeScale = 1;
                gameState = GameState.OnCombat;                
            }
            else if (gameState == GameState.OnCombat)
            {
                UiManager.Instance.ToggleUpgradeUI(true);
                UiManager.Instance.ToggleCustomCursor(false);
                Time.timeScale = 0;
                gameState = GameState.OnUpgrade;
            }
                                         
        }

        if (InputManager.Instance.EscapeInput)
        {
            if (gameState == GameState.OnUpgrade)
            {
                UiManager.Instance.ToggleUpgradeUI(false);
                UiManager.Instance.ToggleCustomCursor(true);
                Time.timeScale = 1;
                gameState = GameState.OnCombat;                
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
            if(gameState == GameState.OnPaused) RestartGame();
        }
    }

    void GameStart()
    {
        //playerShip.enabled = true;        

        gameState = GameState.OnCombat;
        UiManager.Instance.SetCanvas(GameState.OnCombat);
    }

    IEnumerator GameStartCr(float delay)
    {
        if (gameState != GameState.OnTitle) yield break;

        yield return new WaitForSeconds(delay);

        GameStart();
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

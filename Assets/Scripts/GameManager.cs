using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{    
    [Header("for debug")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerShip playerShip;

    private void Awake()
    {
        //playerShip.enabled = false;        

        gameState = GameState.OnTitle;
        UiManager.Instance.SetCanvas(GameState.OnTitle);        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerShip) playerShip = FindObjectOfType<PlayerShip>();

        if (InputManager.Instance.PulseInput)
        {
            if (gameState == GameState.OnTitle)
            {
                playerShip.UsePulse(true);
                playerShip.InitShip();

                StartCoroutine(GameStartCr(0.05f));
            }            
        }

        if (InputManager.Instance.HelpInput)
        {
            UiManager.Instance.ToggleHelpUI(!UiManager.Instance.OnHelp);
        } 

        if (InputManager.Instance.UpgradeInput)
        {
            if (gameState == GameState.OnCombat)
            {
                UiManager.Instance.ToggleUpgradeUI(true);
                Time.timeScale = 0;
                gameState = GameState.OnUpgrade;            
            }
            else if (gameState == GameState.OnUpgrade)
            {
                UiManager.Instance.ToggleUpgradeUI(false);
                Time.timeScale = 1;
                gameState = GameState.OnCombat;
            }                               
        }

        if (InputManager.Instance.EscapeInput)
        {
            if(gameState == GameState.OnUpgrade)
            {
                UiManager.Instance.ToggleUpgradeUI(false);
                Time.timeScale = 1;
                gameState = GameState.OnCombat;
            }
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
}

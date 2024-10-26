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

        if (gameState == GameState.OnTitle && Input.GetKeyDown(KeyCode.F))
        {
            playerShip.UsePulse(true);
            playerShip.InitShip();

            StartCoroutine(GameStartCr(0.05f));
        }
    }

    void GameStart()
    {
        //playerShip.enabled = true;        

        gameState = GameState.StartCombat;
        UiManager.Instance.SetCanvas(GameState.StartCombat);
    }

    IEnumerator GameStartCr(float delay)
    {
        if (gameState != GameState.OnTitle) yield break;

        yield return new WaitForSeconds(delay);

        GameStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform playerShip;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = FindObjectOfType<MoveStandrad>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

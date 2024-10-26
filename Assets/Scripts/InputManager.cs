using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : MonoSingleton<InputManager>
{
    public bool FireInput => Input.GetMouseButton(0);
    public bool MoveForwardInput => Input.GetMouseButton(1);

    Vector2 moveDirectionVec = new Vector2();
    public Vector2 MoveDirectionInput{
        get {
            moveDirectionVec.x = Input.GetAxis("Horizontal");
            moveDirectionVec.y = Input.GetAxis("Vertical");
            return moveDirectionVec;
        } 
    }            

    public bool MissleInput => Input.GetKeyDown(KeyCode.E);
    public bool PulseInput => Input.GetKeyDown(KeyCode.F);
}

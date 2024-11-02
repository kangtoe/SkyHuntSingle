using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : MonoSingleton<InputManager>
{    
    public bool MoveForwardInput => Input.GetMouseButton(1);

    Vector2 moveDirectionVec = new Vector2();
    public Vector2 MoveDirectionInput{
        get {
            moveDirectionVec.x = Input.GetAxis("Horizontal");
            moveDirectionVec.y = Input.GetAxis("Vertical");
            return moveDirectionVec;
        } 
    }

    public bool BrakeInput => Input.GetKey(KeyCode.LeftShift);

    public bool FireInput => Input.GetMouseButton(0);
    public bool MissleInput => Input.GetKeyDown(KeyCode.E);
    public bool PulseInput => Input.GetKeyDown(KeyCode.F);

    public bool HelpInput => Input.GetKeyDown(KeyCode.H);
    public bool UpgradeInput => Input.GetKeyDown(KeyCode.U);
    public bool EscapeInput => Input.GetKeyDown(KeyCode.Escape);

    public bool RInput => Input.GetKeyDown(KeyCode.R);
}

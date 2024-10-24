using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CombatInputManager : MonoSingleton<CombatInputManager>
{
    public bool FireInput => Input.GetMouseButton(0);
    public bool MoveInput => Input.GetMouseButton(1);

    public bool MissleInput => Input.GetKeyDown(KeyCode.E);
    public bool PulseInput => Input.GetKeyDown(KeyCode.F);
}

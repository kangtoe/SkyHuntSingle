using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CombatInputManager : MonoSingleton<CombatInputManager>
{
    public bool FireInput => Input.GetMouseButton(0);
    public bool MoveInput => Input.GetMouseButton(1);

    
}

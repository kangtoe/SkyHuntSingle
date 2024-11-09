using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class LanguageButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemySpawner script = (EnemySpawner)target;

        if (GUILayout.Button(nameof(script.AddSpawnInfo)))
        {
            script.AddSpawnInfo();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        EnemyManager myEnemyManager = (EnemyManager)target;

        if (GUILayout.Button("Spawn"))
        {
            myEnemyManager.SpawnEnemy();
        }
    }
}

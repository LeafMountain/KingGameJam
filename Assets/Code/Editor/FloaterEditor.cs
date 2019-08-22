using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloaterScript))]
public class FloaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawDefaultInspector();

        FloaterScript myFloaterScript = (FloaterScript)target;

        if (GUILayout.Button("Kill"))
        {
            myFloaterScript.OnEaten();
        }
    }
}


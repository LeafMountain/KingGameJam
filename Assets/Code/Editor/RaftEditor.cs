using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RaftScript))]
public class RaftEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

      

        RaftScript myAIBase = (RaftScript)target;

        if (GUILayout.Button("Damage"))
        {
            myAIBase.OnAttacked(1);
        }
    }
}

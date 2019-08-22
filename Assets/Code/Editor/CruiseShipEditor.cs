using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CruiseShipScript))]
public class CruiseShipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawDefaultInspector();

        CruiseShipScript myCruiseShipScript = (CruiseShipScript)target;

        if (GUILayout.Button("Damage"))
        {
            myCruiseShipScript.OnAttacked(1);
        }
    }
}

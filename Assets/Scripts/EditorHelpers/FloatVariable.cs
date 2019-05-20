using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value;

    public void SetValue(float value) => this.value = value;
    public float GetValue() => value;
}

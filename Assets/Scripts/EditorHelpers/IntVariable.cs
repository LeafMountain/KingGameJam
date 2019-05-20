using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/int")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int value;

    public void SetValue(int value) => this.value = value;
    public int GetValue() => value;
}

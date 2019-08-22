using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputMapping : ScriptableObject
{
    public string attackButton = string.Empty;
    public string horizontalAxis = string.Empty;
    public string verticalAxis = string.Empty;

    public bool GetAttack()
    {
        return Input.GetButtonDown(attackButton);
    }

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis)).normalized;
    }
}

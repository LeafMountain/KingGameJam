using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputComponent : MonoBehaviour
{
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";

    public UnityEventVector2 OnMoveInput;

    void Update()
    {
        float h = Input.GetAxisRaw(horizontalAxisName);
        float v = Input.GetAxisRaw(verticalAxisName);
        Vector2 inputDir = new Vector2(h, v).normalized;
        OnMoveInput.Invoke(inputDir);
    } 
}

[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> {}
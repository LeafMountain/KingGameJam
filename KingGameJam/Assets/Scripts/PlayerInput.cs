using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour 
{
	public float2 leftStick;
	public float2 rightStick;

	public bool rightTrigger;
	public bool leftTrigger;

	public bool aButton;
	public bool bButton;
	public bool xButton;
	public bool yButton;

	public UnityVector2Event OnLeftStick;

	void Update ()
	{
		OnLeftStick.Invoke(LeftStick());
	}

	Vector2 LeftStick () 
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		return new Vector2(x, y);
	}
}

[System.Serializable]
public class UnityVector2Event : UnityEvent<Vector2> { }

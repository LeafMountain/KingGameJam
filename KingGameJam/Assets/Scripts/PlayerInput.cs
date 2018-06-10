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

	public UnityEvent OnFireButtonDown;
	public UnityEvent OnFireButtonUp;
	

	void Update ()
	{
		OnLeftStick.Invoke(LeftStick());

		if(Input.GetButtonDown("Fire1")){
			OnFireButtonDown.Invoke();
		} else if (Input.GetButtonUp("Fire1")){
			OnFireButtonUp.Invoke();
		}
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

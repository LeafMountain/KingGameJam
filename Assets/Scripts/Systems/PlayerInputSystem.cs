using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputSystem : ComponentSystem
{
	public struct Data
	{
		public PlayerInput Input;
	}

    protected override void OnUpdate()
    {
		Camera cam = Camera.main;

        foreach (var entity in GetEntities<Data>())
		{
			Vector2 leftStick = LeftStick();
			PlayerInput input = entity.Input;
			input.leftStick = leftStick;

			entity.Input.addBodyPart = Input.GetKey(KeyCode.KeypadPlus);
			entity.Input.removeBodyPart = Input.GetKey(KeyCode.KeypadMinus);

			entity.Input.mousePosition = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
		}
    }

	public float2 LeftStick () 
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		float2 direction = new float2(x, y);



		

		return math.normalize(direction);
	}
}

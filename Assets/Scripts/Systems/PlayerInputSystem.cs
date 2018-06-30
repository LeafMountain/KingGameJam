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
        foreach (var entity in GetEntities<Data>())
		{
			Vector2 leftStick = LeftStick();
			PlayerInput input = entity.Input;
			input.leftStick = leftStick;
		}
    }

	public float2 LeftStick () 
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		return new float2(x, y);
	}
}

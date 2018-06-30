using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem 
{
	public struct Data
	{
		// public Rigidbody2D Rigidbody;
		// public MoveSpeed Speed;
		public PlayerInput Input;
		public Heading Heading;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			// float2 force = entity.Speed.Value * entity.Input.leftStick;
			// entity.Rigidbody.AddForce(force);
			entity.Heading.Value = entity.Input.leftStick;
		}
    }
}

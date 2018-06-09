using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

public class MoveSystem : ComponentSystem {
	public struct Data
	{
		public Rotation2D Rotation;
		public MoveSpeed Speed;
		public Rigidbody2D Rigidbody;
		public PlayerInput Input;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			Quaternion rotation = entity.Rotation.Value;
			float2 direction = new float2(rotation.eulerAngles.x, rotation.eulerAngles.y);

			// position += position + direction * entity.Speed.speed;
			entity.Rigidbody.AddForce(direction * entity.Speed.speed * entity.Input.leftStick);

		}
    }
}

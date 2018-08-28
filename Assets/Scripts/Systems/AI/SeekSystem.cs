using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SeekSystem : ComponentSystem 
{
	public struct Data
	{
		public MoveSpeed Speed;
		public Rigidbody2D Rigidbody;
		public Heading Heading;
		public MoveTarget Target;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			// The desired direction
			Vector2 desired;
			// Amount of force to apply
			Vector2 steer;
			Vector2 futurePosition = entity.Rigidbody.position;

			desired = (Vector2)entity.Target.Value - futurePosition;
			desired = desired.normalized;
			desired *= entity.Speed.maxSpeed;

			// Find the force to apply 
			steer = desired - entity.Rigidbody.velocity;
			steer = steer.normalized * entity.Speed.force;
			steer.y = 0;

			entity.Heading.Value = steer.normalized;
		}
    }
}

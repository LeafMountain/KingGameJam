using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

public class MoveSystem : ComponentSystem 
{
	public struct Data
	{
		public Rigidbody2D Rigidbody;
		public MoveSpeed Speed;
		public Heading Heading;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			if(entity.Rigidbody.velocity.magnitude < entity.Speed.maxSpeed)
			{
				entity.Rigidbody.AddForce(entity.Heading.Value * entity.Speed.force);
			}
		}
    }
}

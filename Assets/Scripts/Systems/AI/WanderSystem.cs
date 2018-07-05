using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class WanderSystem : ComponentSystem
{
	public struct Data
	{
		public Heading Heading;
		public Wander Wander;
		public MoveTarget Target;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			// float wanderAngle = 10;

			float2 addToHeading = new float2();
			// addToHeading.x = math.lerp(-wanderAngle, wanderAngle, Random.Range(0f, 1f)) / wanderAngle;
			// addToHeading.y = math.lerp(-wanderAngle, wanderAngle, Random.Range(0f, 1f)) / wanderAngle;
			addToHeading = (Vector2)entity.Heading.Value;
			

			entity.Target.Value += addToHeading;
		}
    }
}

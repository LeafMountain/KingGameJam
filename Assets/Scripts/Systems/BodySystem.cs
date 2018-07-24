using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BodySystem : ComponentSystem 
{
	public struct Data
	{
		public Follow Follow;
		public Heading Heading;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			Vector2 vectorTowardsParent = entity.Transform.position - entity.Follow.target.position;
			Vector3 targetPosition = vectorTowardsParent.normalized;
			targetPosition *= entity.Follow.distance;
			targetPosition += entity.Follow.target.position;

			entity.Heading.Value = vectorTowardsParent;

			entity.Transform.right = vectorTowardsParent;

			entity.Transform.position = targetPosition;
		}
    }
}

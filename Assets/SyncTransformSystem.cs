using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncTransformSystem : ComponentSystem
{
	public struct HeadingData
	{
		public Heading Heading;
		public Transform Transform;
	}

	public struct PositionData
	{
		public Position Position;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<HeadingData>())
		{
			if((Vector2)entity.Heading.Value != Vector2.zero)
			{
				entity.Transform.right = Vector2.Lerp(entity.Transform.right, (Vector2)entity.Heading.Value, Time.deltaTime * 5);
			}
		}

		foreach (var entity in GetEntities<PositionData>())
		{
			entity.Position.Value = entity.Transform.position;
		}
    }
}

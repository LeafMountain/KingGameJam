using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncTransformSystem : ComponentSystem
{
	public struct Data
	{
		public Heading Heading;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			if((Vector2)entity.Heading.Value != Vector2.zero)
			{
				entity.Transform.right = Vector2.Lerp(entity.Transform.right, (Vector2)entity.Heading.Value, Time.deltaTime * 5);
			}
		}	
    }
}

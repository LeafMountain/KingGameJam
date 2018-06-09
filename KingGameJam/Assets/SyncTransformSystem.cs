using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncTransformSystem : ComponentSystem
{
	public struct Data
	{
		public MyPosition Position;
		public MyHeading Heading;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			entity.Position.Value = entity.Transform.position;
			// entity.Transform.rotation = Quaternion.Euler((Vector2)entity.Heading.Value);
		}	
    }
}

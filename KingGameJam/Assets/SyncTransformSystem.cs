using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms2D;
using UnityEngine;

public class SyncTransformSystem : ComponentSystem {

	public struct Data
	{
		public Transform Transform;
		// [ReadOnly] public Position2D Position;
		[ReadOnly] public Rotation2D Rotation;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			// entity.Transform.position = (Vector2)entity.Position.Value;
			entity.Transform.rotation = Quaternion.Euler((Vector3)(Vector2)entity.Rotation.Value);
		}
    }
}

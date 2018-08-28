using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LoopConstrainSystem : ComponentSystem {

	public struct Data
	{
		public Position Position;
		public LoopConstrain Constrain;
		public Heading Heading;
		public MoveTarget Target;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			Vector3 position = entity.Position.Value;
			Bounds area = new Bounds(Vector2.zero, entity.Constrain.area);

			if(!area.Contains(position))
			{
				// Vector2 towardsBounds = (Vector2)area.ClosestPoint(position) - (Vector2)position;
				entity.Target.Value = (Vector2)area.ClosestPoint(position);
				// towardsBounds = towardsBounds.normalized;
				// entity.Heading.Value = towardsBounds;
			}
		}
    }
}

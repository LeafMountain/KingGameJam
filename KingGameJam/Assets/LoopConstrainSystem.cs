using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LoopConstrainSystem : ComponentSystem {

	public struct Data
	{
		public MyPosition Position;
		public LoopConstrain Constrain;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			Vector3 position = entity.Position.Value;
			Bounds area = new Bounds(Vector2.zero, entity.Constrain.area);

			if(!area.Contains(position))
			{
				// position = -position;
				// entity.Transform.position = position;
			}
		}
    }
}

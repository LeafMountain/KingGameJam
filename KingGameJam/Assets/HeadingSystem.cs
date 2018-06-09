using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HeadingSystem : ComponentSystem 
{
	public struct Data 
	{
		// public Heading Heading;
		public Transform Transform;
		public Rigidbody2D Rigidbody;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			// entity.Transform.LookAt((Vector2)entity.Heading.Value);
		}
    }
}

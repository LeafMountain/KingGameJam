using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;


public class PlayerMoveSystem : ComponentSystem 
{
	public struct Data
	{
		public PlayerInput Input;
		public Heading Heading;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		Camera cam = Camera.main;
		foreach (var entity in GetEntities<Data>())
		{
			// float2 force = entity.Speed.Value * entity.Input.leftStick;
			// entity.Rigidbody.AddForce(force);
			// entity.Heading.Value = entity.Input.leftStick;

			if(Input.GetMouseButton(0))
			{
				Vector2 oldPosition = entity.Transform.position;
				Vector2 mousePos = entity.Input.mousePosition;
				Vector2 newHeading = mousePos - oldPosition;
				entity.Heading.Value = newHeading;
			}
			else
			{
				entity.Heading.Value = new float2();
			}
		}
    }
}

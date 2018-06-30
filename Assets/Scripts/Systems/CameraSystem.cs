using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraSystem : ComponentSystem 
{
	public struct Data
	{
		public Camera Camera;
		public CameraComponent CameraComponent;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			Vector3 velocity = entity.CameraComponent.velocity;
			Vector3 newPos = Vector3.SmoothDamp(entity.Transform.position, entity.CameraComponent.target.position, ref velocity, entity.CameraComponent.smoothing);

			newPos.z = entity.Transform.position.z;

			entity.CameraComponent.velocity = velocity;
			entity.Transform.position = newPos;
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DeathSystem : ComponentSystem 
{
    public struct Data
	{
		public Health Health;
		public Transform Transform;
	}

    protected override void OnUpdate()
    {
		foreach (var entity in GetEntities<Data>())
		{
			if(entity.Health.Value <= 0)
			{
				if(entity.Health.replaceObject)
				{
					GameObject.Instantiate(entity.Health.replaceObject, entity.Transform.position, entity.Transform.rotation);
				}
				
				GameObject.Destroy(entity.Health.gameObject);
			}
		}
    }

}

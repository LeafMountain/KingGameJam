using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class HeadSystem : ComponentSystem
{
	public struct Data
	{
		public Head Head;
		public PlayerInput Input;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			Head head = entity.Head;
			PlayerInput input = entity.Input;

			if(input.addBodyPart)
			{
				head.length ++;
			} 
			else if (input.removeBodyPart)
			{
				head.length --;
			}

			head.length = math.clamp(head.length, 0, 1000);

			if(head.length > head.bodyParts.Count)
			{
				GameObject bodyPart = GameObject.Instantiate(head.bodyPrefab, head.transform.position, quaternion.identity);
				Follow follower = bodyPart.GetComponent<Follow>();
				follower.distance = head.distanceBetweenParts;

				if(head.bodyParts.Count <= 0)
				{
					follower.target = head.transform;
				}
				else 
				{
					follower.target = head.bodyParts[head.bodyParts.Count - 1].transform;
				}

				if(head.bodyParts.Count > 20)
				{
					Damager damager = bodyPart.AddComponent<Damager>();
					damager.damage = 1;
				}

				head.bodyParts.Add(follower);
			}
			else if(head.length < head.bodyParts.Count)
			{
				GameObject bodyPart = head.bodyParts[head.bodyParts.Count - 1].gameObject;
				head.bodyParts.Remove(head.bodyParts[head.bodyParts.Count - 1]);
				GameObject.Destroy(bodyPart);
			}
		}
    }
}

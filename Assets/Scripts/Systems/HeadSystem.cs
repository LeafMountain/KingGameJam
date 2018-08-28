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
		public LineRenderer LineRenderer;
	}

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Data>())
		{
			Head head = entity.Head;
			PlayerInput input = entity.Input;
			List<Follow> bodyParts = entity.Head.bodyParts;

			if(input.addBodyPart)
			{
				head.length ++;
			} 
			else if (input.removeBodyPart)
			{
				head.length --;
			}

			head.length = math.clamp(head.length, 0, 1000);

			if(head.length > bodyParts.Count)
			{
				AddBodyPart(head);
			}
			else if(head.length < bodyParts.Count)
			{
				RemoveBodyPart(head);
			}

			// UpdateLine(bodyParts, entity.LineRenderer);
		}
    }

	void AddBodyPart(Head head)
	{
		// Vector2 spawnPos = (head.bodyParts.Count == 0) ? head.transform.position - head.transform.right * head.distanceBetweenParts : head.bodyParts[head.bodyParts.Count - 1].transform.position - head.bodyParts[head.bodyParts.Count - 1].transform.right * head.distanceBetweenParts;

		GameObject bodyPart = GameObject.Instantiate(head.bodyPrefab, head.transform.position, quaternion.identity);
		Follow follower = bodyPart.GetComponent<Follow>();
		follower.distance = head.distanceBetweenParts;
		Transform target;

		if(head.bodyParts.Count <= 0)
		{
			target = head.transform;
		}
		else 
		{
			target = head.bodyParts[head.bodyParts.Count - 1].transform;
		}

		follower.target = target;
		bodyPart.transform.rotation = follower.target.transform.rotation;
		bodyPart.transform.position -= follower.transform.right * follower.distance;


		if(head.bodyParts.Count > 20)
		{
			Damager damager = bodyPart.AddComponent<Damager>();
			damager.damage = 1;
		}

		head.bodyParts.Add(follower);
	}

	void RemoveBodyPart (Head head)
	{
		GameObject bodyPart = head.bodyParts[head.bodyParts.Count - 1].gameObject;
		head.bodyParts.Remove(head.bodyParts[head.bodyParts.Count - 1]);
		GameObject.Destroy(bodyPart);
	}

	void UpdateLine (List<Follow> bodyParts, LineRenderer lineRenderer)
	{
		lineRenderer.positionCount = bodyParts.Count + 1;

		lineRenderer.SetPosition(0, lineRenderer.transform.position);

		for (int i = 1; i < bodyParts.Count + 1; i++)
		{
			lineRenderer.SetPosition(i, bodyParts[i - 1].transform.position);
		}
	}
}

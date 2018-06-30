using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DamageSystem : ComponentSystem
{
	public struct Receivers
	{
		public int Length;
		public ComponentArray<Health> Health;
		public ComponentArray<CircleCollider2D> collider;
	}

	[Inject] Receivers m_Receivers;

	public struct Damagers
	{
		public int Length;
		public ComponentArray<Damager> Damager;
		public ComponentArray<CircleCollider2D> collider;
	}

	[Inject] Damagers m_Damagers;

    protected override void OnUpdate()
    {
        for (int i = 0; i < m_Receivers.Length; i++)
		{
			Collider2D collider = m_Receivers.collider[i];

			for (int y = 0; y < m_Damagers.Length; y++)
			{
				Collider2D damageCollider = m_Damagers.collider[y];
				
				if(collider.IsTouching(damageCollider))
				{
					Health health = m_Receivers.Health[i];
					health.Value -= m_Damagers.Damager[y].damage;
				}
			}
		}
    }
}

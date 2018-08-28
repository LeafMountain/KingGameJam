using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveTarget : MonoBehaviour 
{
	public float2 Value;

	void OnDrawGizmosSelected ()
	{
		Gizmos.DrawSphere(transform.position, .2f);
	}
}

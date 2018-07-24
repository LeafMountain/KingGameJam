using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Heading : MonoBehaviour 
{
	public float2 Value;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, (Vector2)Value);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour 
{
	public Transform target;
	public float distance;

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, distance);
		Gizmos.DrawLine(transform.position, target.position);
	}
}

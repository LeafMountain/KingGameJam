using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopConstrain : MonoBehaviour {
	public Vector2 area;

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(Vector2.zero, area);
	}
}

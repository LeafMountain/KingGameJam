using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	[Range(0, 1)] public float smoothing;

	Vector3 velocity;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		Vector3 newPos = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothing);
		newPos.z = transform.position.z;

		transform.position = newPos;
	}
}

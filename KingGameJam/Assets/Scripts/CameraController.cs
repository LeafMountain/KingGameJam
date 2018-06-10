using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	[Range(0, 1)] public float smoothing;
	Kraken kraken;

	Vector3 velocity;
	float originalSize;
	Camera cam;

	void Start () 
	{
		kraken = Kraken.Instance;
		originalSize = Camera.main.orthographicSize;
		cam = Camera.main;
	}
	
	void LateUpdate () 
	{
		Kraken kraken = this.kraken.GetComponent<Kraken>();

		Bounds bounds = kraken.GetBounds();

		Vector3 newPos = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothing);

		if(bounds.size.x > originalSize)
		{
			cam.orthographicSize = (bounds.size.x + bounds.size.y) / 2;
		}

		newPos.z = transform.position.z;

		transform.position = newPos;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	[Range(0, 1)] public float smoothing;
	Kraken kraken;

	Vector3 velocity;
	float originalZ;
	Camera camera;

	void Start () 
	{
		kraken = Kraken.Instance;
		originalZ = Camera.main.orthographicSize;
		camera = Camera.main;
	}
	
	void Update () 
	{
		Body kraken = this.kraken.GetComponent<Body>();

		Bounds bounds = GetBounds(kraken);

		// Vector3 zTarget = 

		Vector3 newPos = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothing);

		camera.orthographicSize = originalZ + kraken.currentScale * kraken.scalePerSegment;

		newPos.z = transform.position.z;

		transform.position = newPos;
	}

	Bounds GetBounds (Body body)
	{
		Bounds krakenBounds = new Bounds();
		List<Vector2> positions = new List<Vector2>();

		body.segments.ForEach(segment => positions.Add(segment.position));
		positions.ForEach(pos => krakenBounds.Encapsulate(pos));
		
		return krakenBounds;
	}
}

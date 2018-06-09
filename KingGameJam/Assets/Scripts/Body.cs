using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public static Body Instance;

	public GameObject head;
	public Transform BodyParent;
	public GameObject tail;

	public GameObject bodyPrefab;

	public float segmentDistance = 1.5f;
	public float scalePerSegment = .1f;

	[HideInInspector]
	public float currentScale = 1;

	public List<BodySegment> segments = new List<BodySegment>();
	public List<GameObject> spawnedSegments = new List<GameObject>();

	void Start ()
	{
		currentScale = 1;
		BodySegment head = new BodySegment();
		head.gameObject = this.head;
		segments.Add(head);

		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
	}

	public void AddSegment ()
	{
		BodySegment segment = new BodySegment();
		segment.parent = segments[segments.Count - 1];
		segment.gameObject = Instantiate(bodyPrefab, segment.parent.position - (Vector2)segment.parent.gameObject.transform.up, segment.parent.gameObject.transform.rotation);

		segments.Add(segment);

		currentScale += scalePerSegment;
		ChangeScale(currentScale);
	}

	public void RemoveSegment ()
	{
		if(segments.Count <= 1)
		{
			return;
		}

		Destroy(segments[segments.Count - 1].gameObject);
		segments.RemoveAt(segments.Count - 1);
		currentScale -= scalePerSegment;
		ChangeScale(currentScale);		
	}

	public void ChangeScale (float value)
	{		
		foreach (var segment in segments)
		{
			segment.gameObject.transform.localScale = Vector3.one * value;
		}
	}

	public void UpdateSegments ()
	{
		for (int i = 0; i < segments.Count; i++)
		{
			BodySegment segment = segments[i];

			if(segment.parent == null)
			{
				continue;
			}

			segment.gameObject.transform.up = segment.parent.position - segment.position;
			
			if(Vector3.Distance(segment.position, segment.parent.position) > segmentDistance + (currentScale - 1))
			{
				MoveSegment(segment);
			}
		}
	}

	void MoveSegment (BodySegment segment){
		Vector2 newPos = Vector2.SmoothDamp(segment.position, segment.parent.position, ref segment.velocity, .2f);

		segment.position = newPos;
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			AddSegment();
		}

		if(Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			RemoveSegment();
		}

		UpdateSegments();
	}
}

public class BodySegment
{
	public Vector2 position {
		get {
			return gameObject.transform.position;
		}
		set {
			gameObject.transform.position = value;
		}
	}
	public GameObject gameObject;
	public BodySegment parent;
	public BodySegment child;
	public Vector2 velocity;

	public BodySegment () { }

	public BodySegment (BodySegment parent)
	{
		this.parent = parent;
	}
}

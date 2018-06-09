using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public GameObject head;
	public Transform BodyParent;
	public GameObject tail;

	public GameObject bodyPrefab;

	public float segmentLength;

	List<BodySegment> segments = new List<BodySegment>();
	List<GameObject> spawnedSegments = new List<GameObject>();

	void Start ()
	{
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
		segment.gameObject = Instantiate(bodyPrefab, transform.position, Quaternion.identity);

		segments.Add(segment);		
	}

	public void RemoveSegment ()
	{
		segments.RemoveAt(segments.Count);
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
			
			if(Vector3.Distance(segment.position, segment.parent.position) > segmentLength)
			{
				MoveSegment(segment);
				LineRenderer line = segment.gameObject.GetComponent<LineRenderer>();
				line.SetPosition(0, segment.position);
				line.SetPosition(1, (segment.parent.position));
			}
		}
	}

	void MoveSegment (BodySegment segment){
		// Vector3.SmoothDamp(segment.position, )
		// segment.gameObject.transform.Translate(segment.parent.position - segment.position);
		// segment.gameObject.GetComponent<Rigidbody2D>().AddForce((segment.parent.position - segment.position) * 3);
		Vector2 newPos = Vector2.SmoothDamp(segment.position, segment.parent.position, ref segment.velocity, .5f);
		segment.position = newPos;
	}

	void Update () 
	{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {

	public static Kraken Instance;

	public GameObject head;
	public Transform BodyParent;
	public GameObject tailPrefab;

	public BodySegment tailSegment;

	public GameObject bodyPrefab;

	public float segmentDistance = 1.5f;
	public float scalePerSegment = .1f;

	public float Scale {
		get {
			return segments.Count * scalePerSegment + 1;		
		}
	}

	public float TailLength
	{
		get 
		{
			return segments.Count - 1;
		}
	}

	public List<BodySegment> segments = new List<BodySegment>();
	public List<GameObject> spawnedSegments = new List<GameObject>();

	void Awake ()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}

	void Start ()
	{
		// Create Head
		BodySegment headSegment = new BodySegment();
		headSegment.gameObject = this.head;
		segments.Add(headSegment);

		// Create Tail
		tailSegment = new BodySegment();
		tailSegment.gameObject = Instantiate(tailPrefab, transform.position, Quaternion.identity);
		tailSegment.parent = headSegment;
		segments.Add(tailSegment);
	}

	public void AddSegment ()
	{
		RemoveTail();

		BodySegment segment = new BodySegment();
		segment.parent = segments[segments.Count - 1];
		segment.gameObject = Instantiate(bodyPrefab, segment.parent.position - (Vector2)segment.parent.gameObject.transform.up, segment.parent.gameObject.transform.rotation);
		segment.parent.child = segment;

		segments.Add(segment);

		AddTail();

		UpdateScale();
	}

	public void RemoveSegment ()
	{
		if(segments.Count <= 2)
		{
			return;
		}

		RemoveTail();

		Destroy(segments[segments.Count - 1].gameObject);
		segments.RemoveAt(segments.Count - 1);
		segments[segments.Count - 1].child = null;
		
		AddTail();

		UpdateScale();
	}

	void AddTail ()
	{
		tailSegment.parent = segments[segments.Count - 1];		
		segments.Add(tailSegment);
	}

	void RemoveTail()
	{
		segments.Remove(tailSegment);
		tailSegment.parent = null;
	}

	public void UpdateScale ()
	{
		Vector2 scale = Vector2.one; 
		scale *= this.Scale;

		foreach (var segment in segments)
		{
			segment.gameObject.transform.localScale = scale;
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

			if(segment.child != null)
			{
				segment.gameObject.transform.up = segment.parent.position - segment.child.position;
				UpdateIK(segment);
			}
			else 
			{
				segment.gameObject.transform.up = segment.parent.position - segment.position;
			}

			UpdateFK(segment);
			MoveSegment(segment);

			Vector2 newPos = Vector2.SmoothDamp(segment.position, segment.targetPos, ref segment.velocity, .1f);
			segment.position = newPos;

		}
	}

	void UpdateFK (BodySegment segment)
	{
		float distanceToParent = Vector3.Distance(segment.position, segment.parent.position);
		
		if(distanceToParent > segmentDistance + (Scale - 1))
		{
			segment.targetPos = segment.parent.position + (segment.position - segment.parent.position).normalized * segmentDistance;
		}
	}

	void UpdateIK (BodySegment segment)
	{
		float distanceToChild = Vector3.Distance(segment.position, segment.child.position);

		if(distanceToChild < segmentDistance + (Scale - 1))
		{
			segment.targetPos = segment.parent.position + (segment.parent.position - segment.position).normalized * segmentDistance;
		}
	}

	void MoveSegment (BodySegment segment){
		// Vector2 newPos = Vector2.SmoothDamp(segment.position, segment.parent.position, ref segment.velocity, .5f);
		segment.targetPos = segment.parent.position + (segment.position - segment.parent.position).normalized * segmentDistance;
		Debug.DrawLine(segment.position, segment.targetPos, Color.red);

		// segment.position = newPos;
	}

	public Bounds GetBounds ()
	{
		Bounds bounds = new Bounds();
		bounds.center = transform.position;
		List<Vector2> positions = new List<Vector2>();

		segments.ForEach(segment => positions.Add(segment.position));
		positions.ForEach(pos => bounds.Encapsulate(pos));

		return bounds;
	}

	void LateUpdate () 
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

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube(GetBounds().center, GetBounds().size);
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
	public Vector2 targetPos;

	public BodySegment () { }
}

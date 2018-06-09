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
				Debug.Log("test");				
				segment.gameObject.transform.Translate(segment.position - segment.parent.position);
			}
		}
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
	}
	public GameObject gameObject;
	public BodySegment parent;
	public BodySegment child;

	public BodySegment () { }

	public BodySegment (BodySegment parent)
	{
		this.parent = parent;
	}
}

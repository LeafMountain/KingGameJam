using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {

	public static Kraken Instance;

	public GameObject head;
	public GameObject tailPrefab;

	public BodySegment headSegment;
	public BodySegment tailSegment;

	public GameObject bodyPrefab;

	public float relaxedSpacing = 1.5f;
	public float squeezeSpacing = .5f;
	public float scalePerSegment = .1f;	

	public float attackRange = 1;

	float segmentDistance = 1.5f;

	public float Scale 
	{
		get 
		{
			return segments.Count * scalePerSegment + 1;		
		}
	}

	public float TailLength
	{
		get 
		{
			return segments.Count - 1;
		}
		set
		{
			if(value > TailLength)
			{
				for (int i = 0; i < TailLength - value; i++)
				{
					AddSegment();
				}
			}
			else if (value < TailLength)
			{
				for (int i = 0; i < value - TailLength; i++)
				{
					RemoveSegment();
				}
			}
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
		headSegment = new BodySegment();
		headSegment.gameObject = gameObject;
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
			

			Vector2 newPos = Vector2.SmoothDamp(segment.position, segment.targetPos, ref segment.velocity, .1f);
			segment.position = newPos;
		}
	}

	void UpdateFK (BodySegment segment)
	{
		float distanceToParent = Vector3.Distance(segment.position, segment.parent.position);
		
		if(distanceToParent > segmentDistance + (Scale - 1))
		{
			segment.targetPos = segment.parent.position + (segment.position - segment.parent.position).normalized * (segmentDistance * Scale);
		}
	}

	void UpdateIK (BodySegment segment)
	{
		float distanceToChild = Vector3.Distance(segment.position, segment.child.position);

		if(distanceToChild < segmentDistance + (Scale - 1))
		{
			segment.targetPos = segment.parent.position + (segment.parent.position - segment.position).normalized * (segmentDistance * Scale);
		}
	}

	void MoveSegment (BodySegment segment)
	{
		segment.targetPos = segment.parent.position + (segment.position - segment.parent.position).normalized * segmentDistance;
		Debug.DrawLine(segment.position, segment.targetPos, Color.red);
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

	public void Squeeze()
	{
		tailSegment.child = headSegment;
		headSegment.parent = tailSegment;

		segmentDistance = squeezeSpacing;
		
		Attack();
	}

	public void Relax ()
	{
		tailSegment.child = null;
		headSegment.parent = null;

		segmentDistance = relaxedSpacing;
	}

	void HeadAttack ()
	{
		Ray2D ray = new Ray2D(transform.position, transform.up);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 2);
		Debug.DrawRay(ray.origin, ray.direction * 2, Color.magenta);
		
		DoDamage(hit.transform?.gameObject);
	}

	void Attack ()
	{
		int index = 0;

		foreach (var segment in segments)
		{
			Ray2D rightRay = new Ray2D(segment.position, segment.gameObject.transform.right);
			Ray2D leftRay = new Ray2D(segment.position, -segment.gameObject.transform.right);
			RaycastHit2D rightHit;
			RaycastHit2D leftHit;

			Debug.DrawRay(rightRay.origin, rightRay.direction * attackRange, Color.magenta);
			Debug.DrawRay(leftRay.origin, leftRay.direction * attackRange, Color.magenta);			
			
			rightHit = Physics2D.Raycast(rightRay.origin, rightRay.direction, attackRange);
			leftHit = Physics2D.Raycast(leftRay.origin, leftRay.direction, attackRange);

			if(rightHit.transform)
			{
				DoDamage(rightHit.transform.gameObject);
			}
			if(leftHit.transform)
			{
				DoDamage(leftHit.transform.gameObject);
			}
		}
	}

	void DoDamage (GameObject go)
	{
		go.GetComponent<Health>()?.Damage(1);
	}

	void Update ()
	{
		HeadAttack();
	}

	void LateUpdate () 
	{
		HeadAttack();

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

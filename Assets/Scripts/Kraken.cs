using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {

	public static Kraken Instance;

	public Body body;

	public GameObject head;
	public GameObject tailPrefab;

	public GameObject bodyPrefab;

	public float relaxedSpacing = 1.5f;
	public float squeezeSpacing = .5f;
	public float scalePerSegment = .1f;	

	public float attackRange = 1;

	// float segmentDistance = 1.5f;

	public List<BodySegment> segments = new List<BodySegment>();
	public List<GameObject> spawnedSegments = new List<GameObject>();

	// Rigidbody2D rb;

	void Awake ()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}

	void Start()
	{
		body = new Body(gameObject, tailPrefab, bodyPrefab);

		// rb = GetComponent<Rigidbody2D>();
	}

	public void Squeeze()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		body.tailSegment.child = body.headSegment;
		body.headSegment.parent = body.tailSegment;

		// segmentDistance = squeezeSpacing;
		// Attack();
	}

	public void Relax ()
	{
		body.tailSegment.child = null;
		body.headSegment.parent = null;

		// segmentDistance = relaxedSpacing;
	}

	// void Attack ()
	// {
	// 	foreach (var segment in segments)
	// 	{
	// 		Ray2D rightRay = new Ray2D(segment.position, segment.gameObject.transform.right);
	// 		Ray2D leftRay = new Ray2D(segment.position, -segment.gameObject.transform.right);
	// 		RaycastHit2D rightHit;
	// 		RaycastHit2D leftHit;

	// 		Debug.DrawRay(rightRay.origin, rightRay.direction * attackRange, Color.magenta);
	// 		Debug.DrawRay(leftRay.origin, leftRay.direction * attackRange, Color.magenta);			
			
	// 		rightHit = Physics2D.Raycast(rightRay.origin, rightRay.direction, attackRange);
	// 		leftHit = Physics2D.Raycast(leftRay.origin, leftRay.direction, attackRange);

	// 		// if(rightHit.transform)
	// 		// {
	// 		// 	DoDamage(rightHit.transform.gameObject);
	// 		// }
	// 		// if(leftHit.transform)
	// 		// {
	// 		// 	DoDamage(leftHit.transform.gameObject);
	// 		// }
	// 	}
	// }

	// void DoDamage (GameObject go)
	// {
	// 	go.GetComponent<Health>()?.Damage(1);
	// }

	void LateUpdate () 
	{
		// Attack();

		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			body.AddSegment();
		}

		if(Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			body.RemoveSegment();
		}

		// body.TailLength = GetComponent<Hunger>().HP;
		body.UpdateSegments();
		// Debug.Log(GetComponent<Hunger>().HP);

	}

	void OnDrawGizmos ()
	{
		// Gizmos.DrawWireCube(body.GetBounds().center, body.GetBounds().size);
	}
}

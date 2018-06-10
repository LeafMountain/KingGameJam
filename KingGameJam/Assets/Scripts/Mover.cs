using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour {

	public float force;
	public float maxSpeed;

	Rigidbody2D rigidbody;

	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Move (Vector2 Input)
	{
		if(rigidbody.velocity.magnitude < maxSpeed)
		{
			rigidbody.AddForce(Input * force, ForceMode2D.Force);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsVelocity : MonoBehaviour {

	Rigidbody2D rigidbody;

	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		transform.up = rigidbody.velocity;
	}
}

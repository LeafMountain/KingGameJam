using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsVelocity : MonoBehaviour {

	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		transform.up = rb.velocity;
	}
}

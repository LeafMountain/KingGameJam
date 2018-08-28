using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
	public int damage = 1;

	// void OnCollisionEnter2D(Collision2D col)
	// {
	// 	Health health = col.transform.GetComponent<Health>();

	// 	Debug.Log(col.transform.name);

	// 	if(health)
	// 	{
	// 		health.Value -= damage;
	// 	}
	// }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
	public GameObject go;
	public int number = 1;

	public void Trigger ()
	{
		for (int i = 0; i < number; i++)
		{
			GameObject go2 = Instantiate(go, transform.position, Quaternion.identity);

			go2.GetComponent<Rigidbody2D>()?.AddForce(new Vector2(Random.Range(-1f, 1), (Random.Range(-1f, 1))) * 2, ForceMode2D.Impulse);
		}
	}
}

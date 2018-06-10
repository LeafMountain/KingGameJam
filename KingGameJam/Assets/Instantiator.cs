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
			Instantiate(go, transform.position, Quaternion.identity);
			
		}
	}
}

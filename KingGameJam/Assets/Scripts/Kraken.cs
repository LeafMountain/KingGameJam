using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour {

	public static Kraken Instance;
		
	void Awake ()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else 
		{
			Destroy(gameObject);
		}
	}
}

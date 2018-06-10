using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour {


	public bool debugMode;

	public GameObject floater;

	void Start () {
		
	}
	

	void Update () {
		
		DestroyTest();
	}
	public void DestroyTest(){
	
			new SpawnFloaters(gameObject, floater);
			Destroy(gameObject);
		
	}
	

}

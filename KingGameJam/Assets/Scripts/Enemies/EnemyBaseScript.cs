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
		if(debugMode && Input.GetKeyUp(KeyCode.Space))
		{
			new SpawnFloaters(gameObject, floater);
			Destroy(gameObject);
		}
	}
	

}

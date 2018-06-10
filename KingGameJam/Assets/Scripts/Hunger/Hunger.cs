using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour {

	public float hpDecreaseFactor;
	public Kraken kraken;

	private float hp;




	void Start () {
		
		if(hpDecreaseFactor == 0) hpDecreaseFactor = 1.0f;
		hp = 1.0f;


	}
	

	void Update () {
		
		hp -= time.deltaTime * hpDecreaseFactor;

	}
	
	public void Eat(int tailLength){
		
		hp += 0.1f/(float)tailLength; 
	}
}

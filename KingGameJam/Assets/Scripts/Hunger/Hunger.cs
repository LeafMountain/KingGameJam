using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour {


	
	public Kraken kraken;

	private float hpDropRate = 0.6f;

	private float hp;
	private float timePassed = 0.0f;




	void Start () {
		
		
		if(kraken == null) kraken = Kraken.Instance;

		hp = 1.0f;


	}
	

	void Update () {
		
		HPDrop();
		EatTest();
		timePassed = Time.deltaTime;
	}
	
	public void Eat(){
		
		hp += 0.1f/(float)kraken.tailLength; 
	}
	private void HPDrop(){

		if(timePassed > hpDropRate)
		{
			hp -= 0.01f * kraken.tailLength;
			timePassed = 0.0f;
		}

	}
	private void EatTest()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Eat();
		}

	}
}

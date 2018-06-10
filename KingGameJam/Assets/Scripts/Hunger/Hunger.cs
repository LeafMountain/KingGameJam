using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour {


	[Header("Hunger Properties")]
	public float hpDropRate;
	public float hpAmountLoss;
	public float hpAmountAdd;
	public int smallTailLimit;
	public int bigTailLimit;
	public int tailLossRate;

	public Kraken kraken;

	[Header("Debug")]
	public bool debugMode;
	
	[SerializeField]
	private float hp; //size
	private float timePassed = 0.0f;

	private int debugTailSize = 0;
	private int tailLossCounter;

	void Start () {
		
		
		if(hpDropRate == 0) hpDropRate = 1.2f;
		if(hpAmountLoss == 0) hpAmountLoss = 0.01f;
		if(hpAmountAdd == 0) hpAmountAdd = 1.0f;
		if(smallTailLimit == 0) smallTailLimit = 5;
		if(bigTailLimit == 0) bigTailLimit = 10;
		if(kraken == null) kraken = Kraken.Instance;

		hp = 1.0f;
		tailLossCounter = 0;


	}
	

	void Update () {
		
		HPDrop();
		EatTest();
		LoseTail();
		timePassed += Time.deltaTime;
	}
	
	public void Eat(){
		
		if(!debugMode)
		{
			hp += hpAmountAdd/((float)kraken.body.TailLength + 1.0f);
		}
		else
		{
			hp += hpAmountAdd/((float)debugTailSize + 1.0f);
		} 
	}
	private void HPDrop(){

		if(timePassed > hpDropRate)
		{

			if(!debugMode)
			{hp -= hpAmountLoss * (kraken.body.TailLength + 1.0f);}
			else
			{
				hp -= hpAmountLoss *((float)debugTailSize + 1); 
				
			}
			timePassed = 0.0f;
			tailLossCounter++;

		}

	}
	private void EatTest()
	{
		if(debugMode)
		{
			
			if(Input.GetKeyUp(KeyCode.Space))
			{
				Eat();
				debugTailSize++;
			}
		}
	}
	private void LoseTail(){
		if(tailLossCounter > tailLossRate){
			if(!debugMode)
			{
				if(kraken.body.TailLength > bigTailLimit){
					kraken.body.RemoveSegment(); 
					kraken.body.RemoveSegment(); 
					kraken.body.RemoveSegment();
					}
				else if(kraken.body.TailLength > smallTailLimit){
					kraken.body.RemoveSegment();
				}		
			}
			else
			{
				if(debugTailSize > bigTailLimit){debugTailSize = debugTailSize - 3;}
				else if(debugTailSize > smallTailLimit) {debugTailSize--;}
			}
			
			tailLossCounter = 0;

		}
	}
	//properties
	public float HP {get{return hp;}}

}

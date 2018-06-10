using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBoatMovement : EnemieMovement {

	public float movingTime;
	public float waitTime;
	public float moveSpeed;

	private float timeCounter;
	private bool move;
	private bool pickDir;
	private Vector2 dir;


	void Start () {
		if(movingTime == 0) {movingTime = 1.6f;}
		if(waitTime == 0) {waitTime = 8 * 0.6f;}
		if(moveSpeed == 0) { moveSpeed = 1.0f;}
		if(pickDir) dir = PickDirection();
		timeCounter = 0.0f;
		move = true;
	}
	

	void Update () {
		

		CheckTime();
		if(pickDir) dir = PickDirection();
		Move();
		CheckTime();
		timeCounter += Time.deltaTime;
	}

	private void Move(){

		if(move){
			gameObject.transform.Translate(dir * moveSpeed);
		}

	}
	
	private void CheckTime(){
		if(!move && timeCounter > waitTime){
			move = true;
			timeCounter = 0;
			pickDir = true;
		}
		else if(move && timeCounter > movingTime){
			move = false;
			timeCounter = 0;
		}
	
	}
	private Vector2 PickDirection(){

		pickDir = false;
		return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
		

	}
}

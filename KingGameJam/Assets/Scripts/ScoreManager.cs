using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject scoreDisplay;



	public int scoreForEat;
	public int scoreForRaft;
	public int scoreForMotorBoat;
	public int scoreForDrugBoat;
	public int scoreForCruiseShip;

	private int score;

	public void AddEatScore(){

		score += scoreForEat;

		UpdateScoreDisplay();

	}
	public void AddBoatDestroyScore(GameObject type){

		if(type.name == "MotorBoat(Clone)"){score += scoreForMotorBoat;}
		else if(type.name == "DrugBoat(Clone)"){score += scoreForDrugBoat;}
		else if(type.name == "CruiseShip(Clone)"){score += scoreForCruiseShip;}
		else if(type.name == "Raft(Clone)") {score += scoreForRaft;}

		UpdateScoreDisplay();
	
	}
	private void UpdateScoreDisplay()
	{
		scoreDisplay.GetComponent<Text>().text = score.ToString();
	}

}

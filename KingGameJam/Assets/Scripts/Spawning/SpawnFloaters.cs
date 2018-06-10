using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloaters {

	private GameObject boatDestroyed;
	private GameObject floater;
	private int amountToSpawn;
	private Vector2[] spawnPosistions;

	private SpawnFloaters(){}
	public SpawnFloaters(GameObject boatDestroyed, GameObject floater)
	{
		this.boatDestroyed = boatDestroyed;
		this.floater = floater;

		amountToSpawn = SetSpawnAmount();
		spawnPosistions = SetPositions();
		Spawn();
		
	}

	private int SetSpawnAmount(){
		if(boatDestroyed.name == "Raft(Clone)"){return 2;}
		else if(boatDestroyed.name == "MotorBoat(Clone)"){return 3;}
		else if(boatDestroyed.name == "DrugBoat(Clone)"){return 5;}
		else if(boatDestroyed.name == "CruiseShip(Clone)"){return 10;}

		return 0;
	}
	private Vector2[] SetPositions(){

		Vector2[] newPositions = new Vector2[amountToSpawn];
		Vector2 origoPos = boatDestroyed.transform.position;

		for (int i = 0; i < amountToSpawn; i++)
		{
			float range = Random.Range(0.0f, (float)amountToSpawn)/2.0f;

			Vector2 temp = new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f)).normalized * range;

			newPositions[i] = temp + origoPos;
		}

		return newPositions;
	}
	private void Spawn()
	{
		for (int i = 0; i < amountToSpawn; i++)
		{
			MonoBehaviour.Instantiate(floater, spawnPosistions[i], Quaternion.identity);
		}
		
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour {

	[Header("Enemy List")]
	public GameObject[] enemies;
	public GameObject mainCamera;


	[Header("Seconds between spawns")]
	public int spawningRate;

	[Header("Spawn Marks in Seconds")]
	public float surfer;
	public float raft;
	public float motorBoat;
	public float drugBoat;
	public float cruiseShip;

	private float minXspawningCoordinate = 10.0f;
	private float minYspawningCoordinate = 8.0f;
	private float maxXspawningCoordinate = 20.0f;
	private float maxYspawningCoordinate = 16.0f;

	private float timeElapsed = 0.0f;
	private int mobIndex = 1;
	private float spawningRateCounter = 0;

	void Start () {
		if(mainCamera == null)mainCamera = GameObject.Find("MainCamera");
	}
	

	void Update () {

		timeElapsed += Time.deltaTime;

		if(spawningRateCounter > (float)spawningRate){

			Spawner();
			spawningRateCounter = 0;
		}
		else
		{
			spawningRateCounter += Time.deltaTime;

		}
		if(timeElapsed > surfer && mobIndex == 1) mobIndex++;
		if(timeElapsed > motorBoat && mobIndex == 2) mobIndex++;
		
		
	}
	private void Spawner(){

		Vector2 newPos = SetSpawnCoordinates();

		for (int i = 0; i < mobIndex; i++)
		{
			Spawn(i, newPos);
		} 

	}
	private Vector2 SetSpawnCoordinates(){

		Vector2 krakenPos = Kraken.Instance.transform.position; 

		Vector2 randomPos = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f))*10.0f;

		return krakenPos + randomPos;

	}
	private void Spawn(int mob, Vector2 pos){

		GameObject temp = Instantiate(enemies[mob], pos, Quaternion.identity);
		temp.SetActive(true);

	}

	


}

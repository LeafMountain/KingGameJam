using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour {

	[Header("Enemy List")]
	public GameObject[] enemies;
	public GameObject mainCamera;


	[Header("Seconds between spawns")]
	public int floaterRate;
	public int surferRate;
	public int raftRate;
	public int motorBoatRate;
	public int drugBoatRate;
	public int cruiseShipRate;

	private int[] secTweenSpawns; 

	[Header("Spawn Marks in Seconds")]
	public float floater;
	public float surfer;
	public float raft;
	public float motorBoat;
	public float drugBoat;
	public float cruiseShip;

	private float[] spawnMarks; 

	private float timeElapsed = 0.0f;
	// private int mobIndex = 1;
	private float[] spawningRateCounter;

	void Start () {
		if(mainCamera == null)mainCamera = GameObject.Find("MainCamera");

		secTweenSpawns = new int[] {floaterRate, surferRate, raftRate, motorBoatRate, drugBoatRate, cruiseShipRate};
		spawnMarks = new float[] {floater, surfer, raft, motorBoat, drugBoat, cruiseShip};
		spawningRateCounter = new float[6];
	}
	

	void Update () {

		

		
			Spawner();
	
		
		
		timeElapsed += Time.deltaTime;
		

		for (int i = 0; i < spawningRateCounter.Length; i++)
		{
			spawningRateCounter[i] += Time.deltaTime;
		}
		
	}
	private void Spawner(){

		

		for (int i = 0; i < enemies.Length; i++)
		{
			if(timeElapsed > (float)spawnMarks[i] && spawningRateCounter[i] > secTweenSpawns[i]){
				Vector2 newPos = SetSpawnCoordinates();
				Spawn(i, newPos);
				spawningRateCounter[i] = 0.0f;
			}
		} 

	}
	private Vector2 SetSpawnCoordinates(){

		Vector2 krakenPos = mainCamera.transform.position; 

		float range = Random.Range(12.0f, 20.0f);

		Vector2 randomPos = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized * range;

		return krakenPos + randomPos;

	}
	private void Spawn(int mob, Vector2 pos){

		GameObject temp = Instantiate(enemies[mob], pos, Quaternion.identity);
		temp.SetActive(true);

	}

	


}

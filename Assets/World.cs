using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	Kraken kraken;

	public Vector2 chunksize;
	public GameObject background;
	public GameObject island;

	List<Vector2> chunks = new List<Vector2>();

	void Start () 
	{
		kraken = Kraken.Instance;
		CreateChunk();
	}

	void Update () 
	{
		CreateChunk();
	}

	void CreateChunk()
	{
		Vector2 chunkIndex = kraken.transform.position / chunksize;
		chunkIndex = new Vector2(Mathf.RoundToInt(chunkIndex.x),Mathf.RoundToInt(chunkIndex.y));

		for (int y = -1; y <= 1; y++)
		{
			for (int x = -1; x <= 1; x++)
			{
				AddChunk(new Vector3(chunkIndex.x + x, chunkIndex.y + y));
			}
		}
			
	}

	void AddChunk(Vector2 chunkIndex)
	{
		if(!chunks.Contains(chunkIndex))
		{
			chunks.Add(chunkIndex);
			Vector2 position = chunksize * chunkIndex;
			GameObject go2 = Instantiate(background, position, Quaternion.identity);

			for (int i = 0; i < 5; i++)
			{
				GameObject isle = Instantiate(island, position + new Vector2(RandomFloat(), RandomFloat())* 25, Quaternion.Euler(0,0,RandomFloat() * 360));
				isle.transform.localScale = Vector2.one * Random.Range(.5f, 2f);
			}
		}
	}

	float RandomFloat(){
		return Random.Range(-1f, 1f);
	}
}

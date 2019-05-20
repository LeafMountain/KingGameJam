using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnsPerSecond = 1;
    public Vector2 spawnArea;

    void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(1 / spawnsPerSecond);
        Vector2 randomPos = new Vector2(Random.Range(0, spawnArea.x), Random.Range(0, spawnArea.y));
        randomPos -= spawnArea * .5f;
        randomPos += (Vector2)transform.position;
        GameObject.Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        StartCoroutine(SpawnAfterDelay());
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

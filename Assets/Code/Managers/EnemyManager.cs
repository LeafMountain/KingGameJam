﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager enemyManager;

    private GameManager gameManagerRef;

    public Sprite[] floaterSprites;
    public GameObject floaterPrefab;

    public GameObject[] enemies;

    public GameObject bloodSplatPrefab;
    public GameObject explosionVFXPrefag;
    
    public Vector2 spawnArea = Vector2.one;
    public float spawnInterval = 1;
    public float spawnTimeIncrease = 1;

    [HideInInspector]
    public List<AIBase> enemiesInGame;

    [Header("Debug Spawn")]
    public EnemyType spawn;

    private float timeBetweenSpawn = 1;
    private IEnumerator spawningCoroutine = null;

    void Awake()
    {
        if(enemyManager == null)
        {
            enemyManager = this;
        }
        else
        {
            Destroy(this);
        }
        enemiesInGame = new List<AIBase>();
    }

    public static EnemyManager GetInstance()
    {
        return enemyManager;
    }

    void Start()
    {
        gameManagerRef = GameManager.GetInstance();

        spawningCoroutine = StartSpawning2();
    }

    public static void StartSpawning()
    {
        enemyManager.StartCoroutine(enemyManager.spawningCoroutine);
    }


    public static void StopSpawning()
    {
        enemyManager.StopCoroutine(enemyManager.spawningCoroutine);
    }

    public void SpawnEnemy(int index = -1)
    {
        if(index == -1)
        {
            index = Random.Range(0, enemies.Length);
        }

        GameObject go = Instantiate(enemies[index], GetRandomSpawnPosition(), Quaternion.identity, transform);
    }

    public static void ResetSpawner()
    {
        enemyManager.spawnTimeIncrease = enemyManager.spawnInterval;
    }

    public void DebugSpawnEnemy()
    {
        SpawnEnemy((int)spawn);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        bool xRandom = Random.Range(0, 2) > 0;
        float xPos, yPos;

        if(xRandom == true)
        {
            xPos = Random.Range(0f, spawnArea.x);
            yPos = Random.Range(0, 2) * spawnArea.y;
        }
        else
        {
            xPos = Random.Range(0, 2) * spawnArea.x;
            yPos = Random.Range(0f, spawnArea.y);
        }

        return new Vector2(xPos, yPos) - (spawnArea * .5f);
    }
   
    public void AddToEnemyList(AIBase enemy)
    {
        enemiesInGame.Add(enemy);
    }
    public void RemoveFromEnemyList(AIBase enemy)
    {
        enemiesInGame.Remove(enemy);
    }

    public static Bounds GetPlayArea()
    {
        return new Bounds(enemyManager.transform.position, new Vector3(enemyManager.spawnArea.x, enemyManager.spawnArea.y, 10));
    }

    private IEnumerator StartSpawning2()
    {
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawn);
            timeBetweenSpawn += spawnTimeIncrease;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector2.zero, spawnArea);
    }
}

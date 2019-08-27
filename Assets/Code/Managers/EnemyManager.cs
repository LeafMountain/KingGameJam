using System.Collections;
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

    [HideInInspector]
    public List<AIBase> enemiesInGame;

    [Header("Debug Spawn")]
    public EnemyType spawn;

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
       
    }

    
    void Update()
    {
       // print(enemiesInGame.Count);
    }

    public void SpawnEnemy()
    {
        if(spawn != EnemyType.Length)
        {
            GameObject go =
                Instantiate(enemies[(int)spawn], GetRandomSpawnPosition(), Quaternion.identity);

            go.transform.SetParent(transform);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        gameManagerRef = GameManager.GetInstance();
        Vector2 pos = gameManagerRef.cam.ScreenToWorldPoint(
            new Vector2( Random.Range(0, Screen.height), Random.Range(0, Screen.width)));

        return pos;
    }
   
    public void AddToEnemyList(AIBase enemy)
    {
        print("ADD");
        enemiesInGame.Add(enemy);
    }
    public void RemoveFromEnemyList(AIBase enemy)
    {
        enemiesInGame.Remove(enemy);
    }

}

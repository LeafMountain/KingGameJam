using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Floater, Surfer, Raft, MotorBoat, DrugBoat, CruiseShip, Length}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class AIBase : MonoBehaviour
{
    protected GameManager gameManagerRef;
    protected EnemyManager enemyManagerRef;

   

    public Sprite[] mySprites;

    [Header("Stats")]
    public EnemyType myType;
    public int health;
    public float speed;

    protected SpriteRenderer mySpriteRenderer;
    private int indexTracker;

    void Update()
    {
       
    }

    protected void UpdateSprite()
    {
        if (gameManagerRef.audioManagerRef.beatCount)
        {
            indexTracker++;

            int spriteIndex = indexTracker > 1 ? 0 : 1;

            indexTracker = spriteIndex;

            mySpriteRenderer.sprite = mySprites[indexTracker];
        }
    }

    protected void GetReferences()
    {
        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void SpawnFloaters(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject floater = 
            Instantiate(enemyManagerRef.floaterPrefab, 
                GetSpawnPosition(transform.position), Quaternion.identity);

            floater.transform.SetParent(enemyManagerRef.transform);
        }
    }

    private Vector2 GetSpawnPosition(Vector2 pos)
    {
        Vector2 spawnPosition;

        float x = pos.x + Random.Range(-(int)myType, (int)myType);
        float y = pos.y + Random.Range(-(int)myType, (int)myType);

        spawnPosition = new Vector2(x, y);

        return spawnPosition;
    }

   
    
}

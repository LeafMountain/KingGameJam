using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Floater, Surfer, Raft, MotorBoat, DrugBoat, CruiseShip, Length}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class AIBase : MonoBehaviour
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
}

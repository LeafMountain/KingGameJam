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
    public bool isShip;

    public GameObject mask;
    protected SpriteRenderer mySpriteRenderer;
    protected Vector2 direction;
    private int indexTracker;

    protected bool sinkShake;
    private bool shakeRight;
    protected bool sinking;

    private bool onScreen;

    void Update()
    {
       
    }

    public virtual void Die()
    {
        enemyManagerRef.RemoveFromEnemyList(this);
        sinking = true;

        if (isShip)
        {
            mask.transform.SetParent(transform.parent);
            mySpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            sinkShake = true;

            Destroy(mask,3f);
            Destroy(gameObject,3f);

        }
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

        enemyManagerRef.AddToEnemyList(this);
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

    public virtual void Move()
    {
        if (!sinking)
        {
            transform.position = (Vector2)transform.position + (direction * speed * Time.deltaTime);
        }
    }

    protected Vector2 GetRandomDirection()
    {
       
        Vector2 dir;

        if (onScreen)
        {
            float x = Random.Range(-1.0f, 1.0f);
            float y = Random.Range(-1.0f, 1.0f);

            dir = new Vector2(x, y);

            if (x == 0 && y == 0)
            {
                dir = GetRandomDirection();
            }

            CheckFlipX(direction.x);
        }
        else
        {
            float x = Random.Range(0, Screen.width);
            float y = Random.Range(0, Screen.height);

            dir = gameManagerRef.cam.ScreenToWorldPoint(new Vector2(x, y));

            dir = (dir - (Vector2)transform.position).normalized;

            CheckFlipX(direction.x);
        }

        

        return dir;
    }

    protected void CheckBounderies()
    {
        if (onScreen)
        {
            Vector2 screenPos = gameManagerRef.cam.WorldToScreenPoint(transform.position);

            if (screenPos.x < 0 ||
                screenPos.x > Screen.width)
            {
                direction = new Vector2(-direction.x, direction.y);
                CheckFlipX(direction.x);
            }
            if (screenPos.y < 0 ||
            screenPos.y > Screen.height)
            {
                direction = new Vector2(direction.x, -direction.y);
            }
        }
    }

    protected void CheckFlipX(float x)
    {
        if (myType == EnemyType.Surfer ||
            myType == EnemyType.MotorBoat ||
            myType == EnemyType.DrugBoat ||
            myType == EnemyType.CruiseShip)
        {
            if (x < 0)
            {
                mySpriteRenderer.flipX = true;
            }
            else
            {
                mySpriteRenderer.flipX = false;
            }
        }
    }

    protected void DeathShake()
    {
        if (sinkShake)
        {
            if (shakeRight)
            {
                transform.position = new Vector3(
                    transform.position.x + 0.1f,
                    transform.position.y - (1f * Time.deltaTime));

                shakeRight = !shakeRight;
            }
            else
            {
                transform.position = new Vector3(
                   transform.position.x - 0.1f,
                   transform.position.y- (1f * Time.deltaTime));

                shakeRight = !shakeRight;
            }


            
        }
    }
    protected void CheckIfOnScreen()
    {
        if(!onScreen)
        {
            Vector2 screenPos = gameManagerRef.cam.WorldToScreenPoint(transform.position);

            if (screenPos.x > 0 ||
                screenPos.x < Screen.width ||
                screenPos.y > 0 ||
                screenPos.y < Screen.height)
            {
                onScreen = true;
            }
        }
    }

    protected void ParentUpdate()
    {
        CheckIfOnScreen();
        DeathShake();
        CheckBounderies();
        Move();
    }
    
    

    




}

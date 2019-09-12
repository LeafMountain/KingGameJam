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
    protected AudioManager audioManagerRef;

    public Sprite[] mySprites;

    [Header("Stats")]
    public EnemyType myType;
    public int health;
    public float speed;
    public bool isShip;
    public float length;
    public float height;

    public GameObject mask;
    public GameObject waterParticles;
    public GameObject explosion;
    public AudioClip sinkSFX;

    protected SpriteRenderer mySpriteRenderer;
    protected Vector2 direction;
    private int indexTracker;

    protected bool sinkShake;
    private bool shakeRight;
    protected bool sinking;

    private bool onScreen;

    private Bounds playArea;

    void Update()
    {
       
    }

    public virtual void Die()
    {
        enemyManagerRef.RemoveFromEnemyList(this);
        sinking = true;

        if (isShip)
        {

            mask.SetActive(true);
            mask.transform.SetParent(transform.parent);
            if(waterParticles != null)
            {
                waterParticles.transform.SetParent(transform.parent);
                Destroy(waterParticles, 3f);
            }
            

            mySpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            sinkShake = true;
            GetComponent<Collider2D>().enabled = false;

            if(explosion != null)
            {
                SpawnExplosions();

                Invoke("SpawnExplosions", 0.2f);
                Invoke("SpawnExplosions", 0.5f);
                Invoke("PlaySinkSFX", 1.0f);
            }


            Destroy(mask,3f);
            
            Destroy(gameObject,3f);

        }
        else
        {
            
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

        playArea = EnemyManager.GetPlayArea();
        audioManagerRef = AudioManager.GetInstance();

        if (isShip)
        {
            mask = GetComponentInChildren<SpriteMask>().gameObject;
            mask.SetActive(false);
        }
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

        Vector2 minPlayArea = playArea.min;
        Vector2 maxPlayArea = playArea.max;

        

        
            float x = Random.Range(minPlayArea.x, maxPlayArea.x);
            float y = Random.Range(minPlayArea.y, maxPlayArea.y);


        Vector2 randomPlayAreaPos = new Vector2(x, y);

        dir = (randomPlayAreaPos - (Vector2)transform.position).normalized;

            CheckFlipX(direction.x);

        return dir;
    }

    protected void CheckBounderies()
    {
        if (!playArea.Contains(transform.position) && !MovingTowardsBounds())
        {
           
                    if (transform.position.x < playArea.min.x ||
                        transform.position.x > playArea.max.x)
                    {
                        direction = new Vector2(-direction.x, direction.y);
                        CheckFlipX(direction.x);
                    }

                    if (transform.position.y < playArea.min.y ||
                    transform.position.y > playArea.max.y)
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
                if(waterParticles != null)
                {
                    waterParticles.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                mySpriteRenderer.flipX = false;

                if (waterParticles != null)
                {
                    waterParticles.GetComponent<SpriteRenderer>().flipX = false;
                }
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

    private void SpawnExplosions()
    {
        Vector2 pos;

        float x = Random.Range(transform.position.x - (length / 2),
            transform.position.x + (length / 2));


        float y = Random.Range(transform.position.y - (height/2),
            transform.position.y + (height/2));

        pos = new Vector2(x, y);

        Instantiate(explosion, pos, Quaternion.identity);


    }

    private bool MovingTowardsBounds()
    {
        if ((((Vector2)transform.position + direction) +
            (Vector2)playArea.ClosestPoint(transform.position)).magnitude <
            ((Vector2)transform.position +
                (Vector2)playArea.ClosestPoint(transform.position)).magnitude)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void PlaySinkSFX()
    {
        audioManagerRef.sfxAudio.PlayOneShot(sinkSFX);
    }
   
    
    

    




}

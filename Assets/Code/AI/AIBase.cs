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

    [Header("Stats")]
    public EnemyType myType;
    public int health;
    public float speed;
    public bool isShip;
    public float length;
    public float height;

    [Header("References")]
    public GameObject mask;
    public GameObject waterParticles;
    public GameObject explosion;
    public AudioClip sinkSFX;

    [HideInInspector]
    public SpriteRenderer mySpriteRenderer;

    [HideInInspector]
    public bool sinkShake;
    [HideInInspector]
    public bool shakeRight;
    [HideInInspector]
    public bool sinking;

    [HideInInspector]
    public bool onScreen;

    [HideInInspector]
    public Rigidbody2D body;
   
    public AIMovement aiMovement;
    public AIVisuals aiVisuals;
    public AICheckStatus aiCheckStatus;

    #region Collison
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (aiCheckStatus.DidNotHitEnemy(collision))
        {
            if (aiCheckStatus.IsThistoTheRightOfMe(collision) || aiCheckStatus.IsThisToTheLeftOfMe(collision))
            {
                aiMovement.InvertXDirection();

                if (myType != EnemyType.Floater && myType != EnemyType.Surfer)
                    AIVisuals.FlipSpriteOnX(mySpriteRenderer,
                    waterParticles.GetComponent<SpriteRenderer>(),
                    aiMovement.direction.x, myType);
            }

            if (aiCheckStatus.IsThisAboveMe(collision) || aiCheckStatus.IsThisBeneathMe(collision))
            {
                aiMovement.InvertYDirection();
            }
        }
        else if (aiCheckStatus.HitEnemy(collision) && aiCheckStatus.IsWhatIHitBigger(collision))
        {
            if (aiCheckStatus.IsThistoTheRightOfMe(collision) || aiCheckStatus.IsThisToTheLeftOfMe(collision))
            {
                aiMovement.InvertXDirection();

                if(myType != EnemyType.Floater && myType != EnemyType.Surfer)
                AIVisuals.FlipSpriteOnX(mySpriteRenderer,
                   waterParticles.GetComponent<SpriteRenderer>(),
                   aiMovement.direction.x, myType);
            }

            if (aiCheckStatus.IsThisAboveMe(collision) || aiCheckStatus.IsThisBeneathMe(collision))
            {
                aiMovement.InvertYDirection();
            }
        }
    }

    #endregion
    public virtual void Die()
    {
        enemyManagerRef.RemoveFromEnemyList(this);
        sinking = true;

        body.bodyType = RigidbodyType2D.Kinematic;
        body.velocity = Vector2.zero;


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
    }
   
    protected void SetUp()
    {
        GetReferences();

        aiMovement = new AIMovement(this);
        aiCheckStatus = new AICheckStatus(this);

        if (myType != EnemyType.Floater && myType != EnemyType.Surfer)
            AIVisuals.FlipSpriteOnX(mySpriteRenderer,
               waterParticles.GetComponent<SpriteRenderer>(),
               aiMovement.direction.x, myType);
    }

    protected void GetReferences()
    {
        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();
       
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        enemyManagerRef.AddToEnemyList(this);

        body = GetComponent<Rigidbody2D>();

        audioManagerRef = AudioManager.GetInstance();

        if (isShip)
        {
            mask = GetComponentInChildren<SpriteMask>().gameObject;
            mask.SetActive(false);
        }
    }

    protected void SpawnFloaters(int amount)
    {
        AISpawner.SpawnFloaters(this, amount);
    }
    
    public virtual void Move()
    {
        if (!sinking)
        {
            aiMovement.Move();
        }
    }

    protected void ParentUpdate()
    {
        aiCheckStatus.CheckStatus();
        AIVisuals.DeathShakeAndSink(this);
        Move();
    }

    private void SpawnExplosions()
    {
        AIVisuals.SpawnExplosions(this);
    }

    private void PlaySinkSFX()
    {
        audioManagerRef.sfxAudio.PlayOneShot(sinkSFX);
    }
}

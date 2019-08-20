using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferScript : AIBase, IEdible
{
    private GameManager gameManagerRef;
    private EnemyManager enemyManagerRef;

    public Sprite[] surferSprites;

    public int health;
    public float speed;

    private SpriteRenderer mySpriteRenderer;
    private int indexTracker;

    void Start()
    {

        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = surferSprites[0];
    }

    
    void Update()
    {
        UpdateSprite();
        CheckDeath();
        
    }

    private void UpdateSprite()
    {
        if (gameManagerRef.audioManagerRef.beatCount)
        {
            indexTracker++;

            int spriteIndex = indexTracker > 1 ? 0 : 1;

            indexTracker = spriteIndex;

            mySpriteRenderer.sprite = surferSprites[indexTracker];
        }
    }

    public void OnAttacked(int damage)
    {
        health -= damage;
    }
    private void CheckDeath()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.ExplosionSurfer();

        Destroy(gameObject);

    }

    public void OnEaten()
    {
        Die();
    }
}

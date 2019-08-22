using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferScript : AIBase, IEdible
{
   

    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateSprite();
        CheckDeath();
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

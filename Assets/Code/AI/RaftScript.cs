using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : AIBase, IDamageable
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
    }

    public void OnAttacked(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            SpawnFloaters(2);
        }

        Destroy(gameObject);

    }
}

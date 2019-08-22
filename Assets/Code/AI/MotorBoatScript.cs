using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBoatScript : AIBase, IDamageable
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

        }

    }
}

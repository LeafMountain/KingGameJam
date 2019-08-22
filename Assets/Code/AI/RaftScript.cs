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
        throw new System.NotImplementedException();
    }
}

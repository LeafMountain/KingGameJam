﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugBoatScript : AIBase, IDamageable
{
    

   
    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }

    public void OnAttacked(int damage)
    {
        
    }
}
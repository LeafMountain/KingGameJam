using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugBoatScript : AIBase, IDamageable
{
    

   
    void Start()
    {
        GetReferences();
        direction = GetRandomDirection();
        CheckFlipX(direction.x);
    }

    
    void Update()
    {
        UpdateSprite();
        ParentUpdate();
    }

    public void OnAttacked(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            SpawnFloaters(3);
            base.Die();
        }
        else
        {
            direction = -direction;
            CheckFlipX(direction.x);
        }
    }
   
}

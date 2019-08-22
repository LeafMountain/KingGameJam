using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : AIBase, IDamageable
{
   

    
    void Start()
    {

        GetReferences();
        direction = GetRandomDirection();
    }

    
    void Update()
    {
        UpdateSprite();
        Move();
        CheckBounderies();
    }
    public override void Move()
    {
        base.Move();
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

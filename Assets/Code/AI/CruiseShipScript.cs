using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseShipScript : AIBase, IDamageable
{

  
    void Start()
    {
        SetUp();
    }

   
    void Update()
    {
        ParentUpdate();
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
            base.Die();

            SpawnFloaters(5);
           // Destroy(gameObject);
        }
        else
        {
            SpawnFloaters(2);
            audioManagerRef.OnDamage();
        }
    }
}

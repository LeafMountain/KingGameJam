using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugBoatScript : AIBase, IDamageable
{
    void Start()
    {
        SetUp();
    }

    void Update()
    {
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
            audioManagerRef.OnDamage();
        }
    }
   
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBoatScript : AIBase, IDamageable
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
            SpawnFloaters(2);
            base.Die();
        }
        else
        {
            audioManagerRef.OnDamage();
        }

    }
}

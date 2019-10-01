using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : AIBase, IDamageable
{
    public AudioClip[] deathSFX;

    
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
            SpawnFloaters(2);
        }

        base.Die();

        gameManagerRef.audioManagerRef.sfxAudio.PlayOneShot(
            deathSFX[Random.Range(0, deathSFX.Length)]);

        Destroy(gameObject);

    }
}

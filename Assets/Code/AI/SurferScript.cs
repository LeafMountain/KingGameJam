using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferScript : AIBase, IEdible
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
    }
    private void CheckDeath()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        base.Die();

        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.ExplosionSurfer();

        Destroy(gameObject);

    }

    public override void Move()
    {

        base.Move();
    }

    public void OnEaten()
    {
        Die();
    }
}

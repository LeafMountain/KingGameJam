using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement 
{
    private AIBase aiBase;

    private Bounds playArea;

    public Vector2 direction;

   public AIMovement(AIBase aiBase)
    {
        this.aiBase = aiBase;

        playArea = EnemyManager.GetPlayArea();

        GetRandomDirection();
    }

    public void Move()
    {
        aiBase.transform.position = (Vector2)aiBase.transform.position + (direction * aiBase.speed * Time.deltaTime);
    }

    public Vector2 GetRandomDirection()
    {
       
        Vector2 dir;

        Vector2 minPlayArea = playArea.min;
        Vector2 maxPlayArea = playArea.max;

        float x = Random.Range(minPlayArea.x + 5, maxPlayArea.x - 5);
        float y = Random.Range(minPlayArea.y + 5, maxPlayArea.y - 5);

        Vector2 randomPlayAreaPos = new Vector2(x, y);

        dir = (randomPlayAreaPos - (Vector2)aiBase.transform.position).normalized;

        direction = dir;

        return dir;
    }

    public void InvertYDirection()
    {
        direction = new Vector2(direction.x, -direction.y);
    }

    public void InvertXDirection()
    {
        direction = new Vector2(-direction.x, direction.y);
    }

}   

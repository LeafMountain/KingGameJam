using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner
{
    private static AISpawner aiSpawnerRef;


    public AISpawner()
    {
        if(aiSpawnerRef == null)
        {
            aiSpawnerRef = this;
        }
    }

    public static AISpawner GetInstance()
    {
        return aiSpawnerRef;
    }

    public static void SpawnFloaters(AIBase aiBase, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject floater =
            Object.Instantiate(EnemyManager.GetInstance().floaterPrefab,
                GetSpawnPosition(aiBase.transform.position, aiBase), Quaternion.identity);

            floater.transform.SetParent(EnemyManager.GetInstance().transform);
        }
    }

    private static Vector2 GetSpawnPosition(Vector2 pos, AIBase aiBase)
    {
        Vector2 spawnPosition;

        float x = pos.x + Random.Range(-(int)aiBase.myType, (int)aiBase.myType);
        float y = pos.y + Random.Range(-(int)aiBase.myType, (int)aiBase.myType);

        spawnPosition = new Vector2(x, y);

        return spawnPosition;
    }
}

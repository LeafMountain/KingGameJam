using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager enemyManager;

    public Sprite[] floaterSprites;
    public GameObject floaterPrefab;
    public GameObject bloodSplatPrefab;

    void Awake()
    {
        if(enemyManager == null)
        {
            enemyManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static EnemyManager GetInstance()
    {
        return enemyManager;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

}

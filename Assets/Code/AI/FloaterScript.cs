using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterScript : AIBase , IEdible
{

    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        enemyManagerRef = EnemyManager.GetInstance();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        GetRandomSpriteSet();
        mySpriteRenderer.sprite = mySprites[0];
       
    }
    private void GetRandomSpriteSet()
    {
        int rand = Random.Range(0, 14);

        mySprites = new Sprite[] {enemyManagerRef.floaterSprites[rand *2],
        enemyManagerRef.floaterSprites[rand *2 +1]};

    }

    void Update()
    {
        UpdateSprite();
    }

    public void OnEaten()
    {
        Die();
    }

    private void Die()
    {
        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.ExplosionSurfer();

        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterScript : AIBase , IEdible
{
    public AudioClip eaten;
    

    void Start()
    {
        GetReferences();

        GetRandomSpriteSet();
        mySpriteRenderer.sprite = mySprites[0];

        direction = GetRandomDirection();
      
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
        Move();
        CheckBounderies();

    }

    public void OnEaten()
    {
        Died();
    }

    private void Died()
    {

        base.Die();

        Vector2 myPosition = transform.position;

        Instantiate(enemyManagerRef.bloodSplatPrefab, myPosition, Quaternion.identity);

        gameManagerRef.audioManagerRef.sfxAudio.PlayOneShot(eaten);

        

        Destroy(gameObject);


    }

    public override void Move()
    {
        base.Move();
    }

  
}

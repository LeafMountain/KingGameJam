using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterScript : AIBase , IEdible
{
    public Sprite[] floaterSprites;

    private EnemyManager enemyManagerRef;
    private GameManager gameManagerRef;

    private SpriteRenderer mySpriteRenderer;

    private int indexTracker;
    void Start()
    {
        enemyManagerRef = EnemyManager.GetInstance();
        gameManagerRef = GameManager.GetInstance();

        GetRandomSpriteSet();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = floaterSprites[0];
    }
    private void GetRandomSpriteSet()
    {
        int rand = Random.Range(0, 14);

        floaterSprites = new Sprite[] {enemyManagerRef.floaterSprites[rand *2],
        enemyManagerRef.floaterSprites[rand *2 +1]};

    }

    void Update()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (gameManagerRef.audioManagerRef.beatCount)
        {
            indexTracker++;

            int spriteIndex = indexTracker > 1 ? 0 : 1;

            indexTracker = spriteIndex;

            mySpriteRenderer.sprite = floaterSprites[indexTracker];
        }
    }

    public void OnEaten()
    {
        
    }

    public void OnAttacked(int damage)
    {
      //throw new System.NotImplementedException();
    }
}

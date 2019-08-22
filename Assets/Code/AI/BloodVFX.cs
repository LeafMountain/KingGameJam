using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVFX : MonoBehaviour
{
    public Sprite[] bloodSprites;

    private GameManager gameManagerRef;
    private SpriteRenderer mySpriteRenderer;

    private float alpha = 1;

    private int indexTracker;
    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        UpdateSprite();
        UpdateAlpha();
        
    }
    private void UpdateAlpha()
    {
        if (gameManagerRef.audioManagerRef.beatCount)
        {
            alpha -= .25f;

            mySpriteRenderer.color = new Color(1, 0, 0, alpha);

            if(alpha <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateSprite()
    {
        if (gameManagerRef.audioManagerRef.beatCount)
        {
            indexTracker++;

            int spriteIndex = indexTracker > 1 ? 0 : 1;

            indexTracker = spriteIndex;

            mySpriteRenderer.sprite = bloodSprites[indexTracker];
        }
    }
}

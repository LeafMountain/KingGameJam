using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVFX : MonoBehaviour
{
    public Sprite[] bloodSprites;

    private GameManager gameManagerRef;
    private SpriteRenderer mySpriteRenderer;

    private int indexTracker;
    void Start()
    {
        gameManagerRef = GameManager.GetInstance();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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

            mySpriteRenderer.sprite = bloodSprites[indexTracker];
        }
    }
}

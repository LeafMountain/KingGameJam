using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICheckStatus 
{
    private Transform transform;

    private AIBase aiBase;
    private Bounds playArea;

    public AICheckStatus(AIBase aiBase)
    {
        this.aiBase = aiBase;
        transform = aiBase.transform;

        playArea = EnemyManager.GetPlayArea();
    }

    public void CheckStatus()
    {
        CheckBounderies();
        CheckIfOnScreen();
    }

    public bool IsThisBeneathMe(Collision2D collision)
    {
        return transform.position.y > collision.transform.position.y;
    }

    public bool IsThisAboveMe(Collision2D collision)
    {
        return transform.position.y < collision.transform.position.y;
    }

    public bool IsThisToTheLeftOfMe(Collision2D collision)
    {
        return transform.position.x > collision.transform.position.x;
    }

    public bool IsThistoTheRightOfMe(Collision2D collision)
    {
        return transform.position.x < collision.transform.position.x;
    }

    public bool IsWhatIHitBigger(Collision2D collision)
    {
        return (int)collision.gameObject.GetComponent<AIBase>().myType > (int)aiBase.myType;
    }

    public bool HitEnemy(Collision2D collision)
    {
        return collision.gameObject.tag == "Enemy";
    }

    public bool DidNotHitEnemy(Collision2D collision)
    {
        return collision.gameObject != null && collision.gameObject.tag != "Enemy";
    }

    public void CheckBounderies()
    {
        if (!MovingTowardsBounds())
        {

            if (aiBase.transform.position.x < playArea.min.x ||
                aiBase.transform.position.x > playArea.max.x)
            {
                aiBase.aiMovement.InvertXDirection();

                AIVisuals.FlipSpriteOnX(aiBase.mySpriteRenderer,
                aiBase.waterParticles.GetComponent<SpriteRenderer>(),
                aiBase.aiMovement.direction.x, aiBase.myType);

                return;
            }

            if (aiBase.transform.position.y < playArea.min.y ||
            aiBase.transform.position.y > playArea.max.y - 5)
            {

                aiBase.aiMovement.InvertYDirection();

                return;
            }
        }

        if (aiBase.transform.position.y > playArea.max.y - 10 && aiBase.body.velocity.y > 0)
        {
            aiBase.aiMovement.InvertYDirection();
        }
    }

    private bool MovingTowardsBounds()
    {
        if ((((Vector2)aiBase.transform.position + aiBase.aiMovement.direction) +
            (Vector2)playArea.ClosestPoint(aiBase.transform.position)).magnitude <
            ((Vector2)aiBase.transform.position +
                (Vector2)playArea.ClosestPoint(aiBase.transform.position)).magnitude)
        {
            return true;
        }
        else
        {

            return false;
        }
    }

    private void CheckIfOnScreen()
    {
        if (!aiBase.onScreen)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.x > 0 ||
                screenPos.x < Screen.width ||
                screenPos.y > 0 ||
                screenPos.y < Screen.height)
            {
                aiBase.onScreen = true;
            }
        }
    }
}

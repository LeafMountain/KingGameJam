using UnityEngine;

public class AIVisuals 
{
    private static AIVisuals aiVisualsRef;

    public AIVisuals()
    {
        if(aiVisualsRef == null)
        {
            aiVisualsRef = this;
        }
    }

    public static AIVisuals GetInstance()
    {
        return aiVisualsRef;
    }

    public static void FlipSpriteOnX(SpriteRenderer boatSprite, SpriteRenderer waterParticles, float directionXValue, EnemyType type)
    {
        if (type == EnemyType.MotorBoat ||
                 type == EnemyType.DrugBoat ||
                 type == EnemyType.CruiseShip ||
                 type == EnemyType.Surfer)
        {

            if (directionXValue < 0)
            {
                boatSprite.flipX = true;

                if (type != EnemyType.Surfer && waterParticles != null)
                {
                    waterParticles.flipX = true;
                }
            }
            else
            {
                boatSprite.flipX = false;

                if (type != EnemyType.Surfer &&  waterParticles != null)
                {
                    waterParticles.flipX = false;
                }
            }
        }
        
    }

    public static void DeathShakeAndSink(AIBase aiBase)
    {
        if (aiBase.sinkShake)
        {
            if (aiBase.shakeRight)
            {
                aiBase.transform.position = new Vector3(
                    aiBase.transform.position.x + 0.1f,
                    aiBase.transform.position.y - (1f * Time.deltaTime));

                aiBase.shakeRight = !aiBase.shakeRight;
            }
            else
            {
                aiBase.transform.position = new Vector3(
                   aiBase.transform.position.x - 0.1f,
                   aiBase.transform.position.y - (1f * Time.deltaTime));

                aiBase.shakeRight = !aiBase.shakeRight;
            }
        }
    }

    public static void SpawnExplosions(AIBase aiBase)
    {
        Vector2 pos;

        float x = Random.Range(aiBase.transform.position.x - (aiBase.length / 2),
            aiBase.transform.position.x + (aiBase.length / 2));


        float y = Random.Range(aiBase.transform.position.y - (aiBase.height / 2),
            aiBase.transform.position.y + (aiBase.height / 2));

        pos = new Vector2(x, y);

        Object.Instantiate(aiBase.explosion, pos, Quaternion.identity);
    }
}

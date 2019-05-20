using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Eater : MonoBehaviour
{
    [Range(0, 180)]
    public float eatAngle = 45;

    void OnCollisionEnter2D(Collision2D col)
    {
        IEdible edible = col.transform.GetComponent<IEdible>();
        if (edible != null)
        {
            Vector2 hitPoint = col.GetContact(0).point;
            Vector2 hitPointDir = hitPoint - (Vector2)transform.position;
            float angleToHit = Vector2.Angle(hitPointDir, transform.right);
            Debug.Log(angleToHit);

            // eat 
            edible.Eat(this);
        }
    }
}

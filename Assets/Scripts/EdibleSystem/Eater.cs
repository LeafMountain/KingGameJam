using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Eater : MonoBehaviour
{
    [Range(0, 180)]
    public float eatAngle = 45;

    public IntVariable points;

    void Awake()
    {
        points.SetValue(0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        IEdible edible = col.transform.GetComponent<IEdible>();
        if (edible != null)
        {
            Vector2 hitPoint = col.GetContact(0).point;
            Vector2 hitPointDir = hitPoint - (Vector2)transform.position;
            float angleToHit = Vector2.Angle(hitPointDir, transform.right);

            // eat 
            edible.Eat(this);
        }
    }

    public void GivePoints(int value) => points.SetValue(points.GetValue() + value);

    void OnDrawGizmos()
    {
        Handles.DrawSolidArc(transform.position, transform.right, transform.right, eatAngle, 1);
    }
}

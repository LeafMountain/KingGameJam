using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EdibleHuman : MonoBehaviour, IEdible
{
    public int pointsValue;

    public void Eat(Eater instagator)
    {
        instagator.GivePoints(pointsValue);
        Destroy(gameObject);
    }

}

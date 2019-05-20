using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EdibleHuman : MonoBehaviour, IEdible
{
    public void Eat(Eater instagator)
    {
        Debug.Log("FUUUCK YOU ATE ME!!");
        Destroy(gameObject);
    }

}

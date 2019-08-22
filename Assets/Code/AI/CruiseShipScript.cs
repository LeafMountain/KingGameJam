using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseShipScript : AIBase, IDamageable
{
    

  
    void Start()
    {
        GetReferences();
    }

   
    void Update()
    {
        UpdateSprite();
    }

    public void OnAttacked(int damage)
    {
        
    }
}

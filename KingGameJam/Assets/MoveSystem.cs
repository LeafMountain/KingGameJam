using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MoveSystem : ComponentSystem 
{

	public struct Data
	{
		public Rigidbody2D Rigidbody;
	}

    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}

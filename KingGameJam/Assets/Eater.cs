using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Eater : MonoBehaviour 
{
	public float eatRange;
	public UnityEvent OnEat;

	void Update ()
	{
		Ray2D eatRay = new Ray2D(transform.position, transform.up);
		RaycastHit2D hit = Physics2D.Raycast(eatRay.origin, eatRay.direction, eatRange);
		
		if(hit.transform)
		{
			Food food = hit.transform.GetComponent<Food>();

			if(food)
			{
				food.Eat();
			}
		}
	}
}

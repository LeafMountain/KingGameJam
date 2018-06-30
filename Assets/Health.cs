using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
	public int Value;
	int queuedDamage;

	public UnityEvent OnDead;

	public void Damage (int value)
	{
		queuedDamage += value;
	}

	void LateUpdate ()
	{
		if(queuedDamage >= Value)
		{
			OnDead.Invoke();
		}
		else
		{
			queuedDamage = 0;
		}
	}
}

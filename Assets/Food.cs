using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour {
	public float Value;
	public UnityEvent OnEaten;

	public void Eat()
	{
		OnEaten.Invoke();
	}
}

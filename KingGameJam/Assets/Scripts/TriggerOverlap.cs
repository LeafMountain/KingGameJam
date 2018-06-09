using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOverlap : MonoBehaviour {

	public UnityGOEvent OnEnter;
	public UnityGOEvent OnExit;
	public LayerMask mask;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.layer == mask)
		{
			OnEnter.Invoke(col.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if(col.gameObject.layer == mask)
		{
			OnExit.Invoke(col.gameObject);
		}		
	}
}

[System.Serializable]
public class UnityGOEvent : UnityEvent<GameObject> { }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMessage : MonoBehaviour {

	public void Trigger (string message)
	{
		Debug.Log(message);
	}
}

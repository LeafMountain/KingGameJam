using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {
	public int length;
	public List<Follow> bodyParts = new List<Follow>();
	public GameObject bodyPrefab;
	public float distanceBetweenParts = .1f;
}

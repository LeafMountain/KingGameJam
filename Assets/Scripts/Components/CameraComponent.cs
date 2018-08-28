using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraComponent : MonoBehaviour {
	public Transform target;
	[Range(0, 1)]
	public float smoothing = .5f;
	public float3 velocity;
}

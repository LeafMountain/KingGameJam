using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour 
{
	public float2 mousePosition;
	public float2 leftStick;
	public bool addBodyPart;
	public bool removeBodyPart;
}
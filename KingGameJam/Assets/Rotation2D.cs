using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Rotation2D : IComponentData 
{
	public float2 Value;
}

public class Rotation2DComponent : ComponentDataWrapper<Rotation2D> { }

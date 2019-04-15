using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct TestStruct
{
    public float4[] colors;
}

[Serializable]
public struct TestComponent : IComponentData
{
    public NativeArray<TestStruct> texture;
}

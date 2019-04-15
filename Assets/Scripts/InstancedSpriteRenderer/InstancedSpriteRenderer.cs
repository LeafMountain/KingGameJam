using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public unsafe struct RenderSprite : ISharedComponentData
{
    public Texture sprite;

    // public CustomSprite sprite;
    // public Texture2D texture;
    // public DynamicBuffer<float4> colors;
    // public NativeArray<int> test;
    // public void* colors;
    // public Texture texture;
    // public List<int> colors;
    // public Mesh mesh;
    // public Material material;
}

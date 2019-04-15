using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;

public struct CustomSprite
{
    // public float4[] colors;
    // public NativeArray<float4> colors;
    // public DynamicBuffer<float4> colors;
    public List<float4> colors;
    public float width;
    public float height;
    public float2 pivot;


    public CustomSprite(float4[] colors, float width, float height, float2 pivot)
    {
        // this.colors = new NativeArray<float4>(colors, Allocator.Persistent);
        // this.colors = colors;
        // this.colors = new DynamicBuffer<float4>();
        // NativeArray<float4> colorNative = new NativeArray<float4>(colors, Allocator.Temp);
        // this.colors.Add(colors[0]);
        // colorNative.Dispose();

        this.colors = new List<float4>(colors.Length);
        this.width = width;
        this.height = height;
        this.pivot = pivot;
    }
}

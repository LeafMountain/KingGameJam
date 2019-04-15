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
}

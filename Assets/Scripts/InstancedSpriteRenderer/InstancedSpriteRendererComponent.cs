using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class InstancedSpriteRendererComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public Texture2D sprite;

    [LayerField]
    public int layer;
    public UnityEngine.Rendering.ShadowCastingMode castShadows;
    public bool receiveShadows;
    public int spriteIndex = 0;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Sprite[] sprites = sprite.CreateSpriteSheet();
        // Material mat = new Material(Shader.Find("Sprites/Instanced"));
        // mat.mainTexture = sprites[spriteIndex].ToTexture();
        // float aspectRatio = mat.mainTexture.width / mat.mainTexture.height;
        // Mesh mesh = MeshUtils.GenerateQuad(aspectRatio, 1, sprites[spriteIndex].pivot / sprites[spriteIndex].pixelsPerUnit);

        // dstManager.AddSharedComponentData(entity, new RenderMesh
        // {
        //     mesh = mesh,
        //     material = mat,
        //     subMesh = 0,
        //     layer = this.layer,
        //     castShadows = this.castShadows,
        //     receiveShadows = this.receiveShadows
        // });
        float4[] colors = sprites[spriteIndex].ToTexture().GetPixelsFloat4();
        RenderSprite isr = new RenderSprite();
        NativeArray<float4> test = new NativeArray<float4>(colors, Allocator.Persistent);
        // isr.colors.AddRange(colors);
        // RenderMesh teststs;

        // unsafe{
        //     isr.colors = test.GetUnsafePtr();
        //     // void* aptr;
        // }
        // isr.test = test;
        // isr.test = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray(colors, colors.Length, Allocator.Persistent);
        // isr.colors = new DynamicBuffer<float4>();

        // NativeArray<float4> nativeColors = new NativeArray<float4>(colors, Allocator.Temp);
        // isr.colors.AddRange(nativeColors);
        
        // for (int i = 0; i < colors.Length; i++)
        // {
        //     isr.colors.Add(colors[i]);
        // }

        // dstManager.AddComponentData(entity, isr);
        test.Dispose();

        // nativeColors.Dispose();
    }

    CustomSprite SpriteToCustomSprite(Texture2D texture)
    {
        CustomSprite sprite = new CustomSprite(texture.GetPixelsFloat4(), texture.width, texture.height, new float2(.5f, .5f));
        return sprite;
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector2.one);
    }
}

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
    public int spriteIndex = 0;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Sprite[] sprites = sprite.CreateSpriteSheet();
        dstManager.AddSharedComponentData(entity, new RenderSprite
        {
            sprite = sprites[spriteIndex].ToTexture()
        });

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
    }

    // CustomSprite SpriteToCustomSprite(Texture2D texture)
    // {
    //     CustomSprite sprite = new CustomSprite(texture.GetPixelsFloat4(), texture.width, texture.height, new float2(.5f, .5f));
    //     return sprite;
    // }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector2.one);
    }
}

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
        Material mat = new Material(Shader.Find("Sprites/Instanced"));
        mat.mainTexture = sprites[spriteIndex].ToTexture();
        float aspectRatio = mat.mainTexture.width / mat.mainTexture.height;


        Mesh mesh = MeshUtils.GenerateQuad(aspectRatio, 1, sprites[spriteIndex].pivot / sprites[spriteIndex].pixelsPerUnit);

        dstManager.AddSharedComponentData(entity, new RenderMesh
        {
            mesh = mesh,
            material = mat,
            subMesh = 0,
            layer = this.layer,
            castShadows = this.castShadows,
            receiveShadows = this.receiveShadows
        });

        // dstManager.AddSharedComponentData(entity, new InstancedSpriteRenderer
        // {
        //     sprite = sprites[0]
        // });
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector2.one);
    }
}

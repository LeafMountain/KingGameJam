using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class InstancedSpriteRendererComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public Texture2D sprite;
    public int pixelsPerUnit;
    public float2 pivot;

    [LayerField]
    public int layer;
    public UnityEngine.Rendering.ShadowCastingMode castShadows;
    public bool receiveShadows;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Sprite[] sprites = sprite.CreateSpriteSheet(new float2(0));

        Material mat = new Material(Shader.Find("Sprites/Instanced"));
        mat.mainTexture = sprites[0].ToTexture();

        float aspectRatio = mat.mainTexture.width / mat.mainTexture.height;

        Mesh mesh = MeshUtils.GenerateQuad(aspectRatio, 1, Vector2.one * .25f);

        dstManager.AddSharedComponentData(entity, new RenderMesh
        {
            mesh = mesh,
            material = mat,
            subMesh = 0,
            layer = this.layer,
            castShadows = this.castShadows,
            receiveShadows = this.receiveShadows
        });
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector2.one);
    }
}

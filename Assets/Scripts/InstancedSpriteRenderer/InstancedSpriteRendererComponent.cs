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

    // public Mesh mesh;
    // public Material material;
    // public int subMesh;
    [LayerField]
    public int layer;
    public UnityEngine.Rendering.ShadowCastingMode castShadows;
    public bool receiveShadows;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Mesh mesh = MeshUtils.GenerateQuad(1, Vector2.zero);
        Material mat = new Material(Shader.Find("Sprites/Instanced"));
        mat.mainTexture = this.sprite;

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
        Gizmos.DrawMesh(MeshUtils.GenerateQuad(1, Vector2.zero));
    }
}

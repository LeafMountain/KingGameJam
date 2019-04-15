using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class InstancedSpriteRendererSystem : ComponentSystem
{

    // This should ONLY gather all the sprite renderers
    // Separate this into it's own thing
    [BurstCompile]
    struct InstancedSpriteRendererSystemJob : IJobParallelFor
    {
        // Gather all the RenderSprite components

        [ReadOnly] public NativeArray<ArchetypeChunk> Chunks;
        [ReadOnly] public ArchetypeChunkSharedComponentType<RenderSprite> RenderSpriteType;
        public NativeArray<int> ChunkRenderer;

        public void Execute(int chunkIndex)
        {
            ArchetypeChunk chunk = Chunks[chunkIndex];
            int sharedIndex = chunk.GetSharedComponentIndex(RenderSpriteType);
            ChunkRenderer[chunkIndex] = sharedIndex;
        }
    }

    protected override void OnUpdate()
    {
        NativeArray<RenderSprite> renderers;
        // var entities = GetEntityQuery(ComponentType);
        // Render the sprites (fuck error checking)
        throw new System.NotImplementedException();
    }
}
        // public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref InstancedSpriteRenderer renderer)
        // {
        //     // UnityEngine.Debug.Log("hello");
        //     // Matrix4x4[] matricesArray = new Matrix4x4[1];
        //     // matricesArray[0].SetTRS(translation.Value, rotation.Value, Vector2.one);
        //     // Mesh mesh = MeshUtils.GenerateQuad(10, Vector2.zero);
        //     Material material = new Material(Shader.Find("Sprites/Instanced"));

        //     // Graphics.DrawMeshInstanced(mesh, 0, material, matricesArray);
        //     // Graphics.DrawTexture(new Rect(10, 10, 10, 10), 
        // }
    // protected override void OnUpdate(JobHandle inputDependencies)
    // {
    //     var job = new InstancedSpriteRendererSystemJob();
    //     return job.Schedule(this, inputDependencies);
    // }
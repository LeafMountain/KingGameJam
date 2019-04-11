using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class InstancedSpriteRendererSystem : JobComponentSystem
{
    [BurstCompile]
    struct InstancedSpriteRendererSystemJob : IJobForEach<Translation, Rotation, InstancedSpriteRenderer>
    {
        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref InstancedSpriteRenderer renderer)
        {
            Matrix4x4[] matricesArray = new Matrix4x4[1];
            matricesArray[0].SetTRS(translation.Value, rotation.Value, Vector2.one);
            Mesh mesh = MeshUtils.GenerateQuad(10, Vector2.zero);
            Material material = new Material(Shader.Find("Sprites/Instanced"));

            Graphics.DrawMeshInstanced(mesh, 0, material, matricesArray);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new InstancedSpriteRendererSystemJob();
        return job.Schedule(this, inputDependencies);
    }
}
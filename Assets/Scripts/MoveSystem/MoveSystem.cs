using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class MoveSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveSystemJob : IJobForEach<Translation, Rotation, Velocity>
    {
        public float deltaTime;

        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, ref Velocity velocity)
        {
            translation.Value += velocity.Value * deltaTime;

            // rotation.Value = Quaternion.LookRotation(velocity.Value, Vector3.back);
            // math.atan2(velocity.Value.y, velocity.Value.x)
            // Vector3.Angle(velocity.Value, Vector3.right);

            Quaternion currentRotation = rotation.Value;
            float angle = math.atan2(velocity.Value.y, velocity.Value.x);
            angle *= Mathf.Rad2Deg;
            angle = angle < 0 ? 360 + angle : angle;

            // float smoothAngle = math.lerp(currentRotation.eulerAngles.z, angle, deltaTime * 10);
            // UnityEngine.Debug.Log(smoothAngle);

            // Should happen earlier
            if ((velocity.Value.x != 0 & velocity.Value.y != 0) || (velocity.Value.x == 0 && velocity.Value.y != 0) || (velocity.Value.x != 0 && velocity.Value.y == 0))
                rotation.Value = Quaternion.Lerp(rotation.Value, Quaternion.Euler(0, 0, angle), deltaTime * 5);

            // rotation.Value = Quaternion.LookRotation(math.normalize(velocity.Value), Vector3.forward);

            // UnityEngine.Debug.Log(rotation.Value.value);
            // translation.Value += new float3(1, 0, 0) * deltaTime;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MoveSystemJob();
        job.deltaTime = UnityEngine.Time.deltaTime;
        return job.Schedule(this, inputDependencies);
    }
}
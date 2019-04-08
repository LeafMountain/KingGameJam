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
    // This declares a new kind of job, which is a unit of work to do.
    // The job is declared as an IJobForEach<Translation, Rotation>,
    // meaning it will process all entities in the world that have both
    // Translation and Rotation components. Change it to process the component
    // types you want.
    //
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    [BurstCompile]
    struct MoveSystemJob : IJobForEach<Translation, Rotation, Velocity>
    {
        // Add fields here that your job needs to do its work.
        // For example,
        public float deltaTime;

        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, ref Velocity velocity)
        {
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job. Note that the 'rotation' parameter is
            // marked as [ReadOnly], which means it cannot be modified,
            // but allows this job to run in parallel with other jobs
            // that want to read Rotation component data.
            // For example,
            //     translation.Value += mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
            translation.Value += velocity.Value * deltaTime;

            // rotation.Value = Quaternion.LookRotation(velocity.Value, Vector3.back);
            // math.atan2(velocity.Value.y, velocity.Value.x)
            // Vector3.Angle(velocity.Value, Vector3.right);

            Quaternion currentRotation = rotation.Value;
            float angle = math.atan2(velocity.Value.y, velocity.Value.x);
            angle *= Mathf.Rad2Deg;

            float smoothAngle = math.lerp(currentRotation.eulerAngles.z, angle, deltaTime * 10);
            // UnityEngine.Debug.Log(smoothAngle);

            if ((velocity.Value.x != 0 && velocity.Value.y != 0) || (velocity.Value.x == 0 && velocity.Value.y != 0) || (velocity.Value.x != 0 && velocity.Value.y == 0))
                rotation.Value = Quaternion.Euler(0, 0, smoothAngle);

            // rotation.Value = Quaternion.LookRotation(math.normalize(velocity.Value), Vector3.forward);

            // UnityEngine.Debug.Log(rotation.Value.value);
            // translation.Value += new float3(1, 0, 0) * deltaTime;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MoveSystemJob();

        // Assign values to the fields on your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        job.deltaTime = UnityEngine.Time.deltaTime;

        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}
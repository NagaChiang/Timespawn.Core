using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenMovementCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle job = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenMovement tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenMovement>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
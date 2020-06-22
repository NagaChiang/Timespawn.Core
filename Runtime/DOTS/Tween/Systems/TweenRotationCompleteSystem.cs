using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenRotationCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle job = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenRotation tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenRotation>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
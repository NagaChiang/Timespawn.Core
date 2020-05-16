using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenPostCompleteSystemGroup))]
    public class TweenRotationPostCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.WithAll<TweenRotationCompleteTag>().ForEach((Entity entity, int entityInQueryIndex) =>
            {
                endSimulationCommandBuffer.RemoveComponent<TweenRotationCompleteTag>(entityInQueryIndex, entity);
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
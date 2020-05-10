using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenPostCompleteSystemGroup))]
    public class TweenRotationPostCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EndSimulationEntityCommandBufferSystem endSimulationECBSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent commandBuffer = endSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenRotationCompleteTag tag) =>
            {
                commandBuffer.RemoveComponent<TweenRotationCompleteTag>(entityInQueryIndex, entity);
            }).Schedule(inputDeps);

            endSimulationECBSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenRotationCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            BeginSimulationEntityCommandBufferSystem beginSimulationECBSystem = DotsUtils.GetSystemFromDefaultWorld<BeginSimulationEntityCommandBufferSystem>();
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent beginSimulationCommandBuffer = beginSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenRotationData tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenMovementData>(entityInQueryIndex, entity);
                    beginSimulationCommandBuffer.AddComponent(entityInQueryIndex, entity, new TweenRotationCompleteTag());
                    endSimulationCommandBuffer.RemoveComponent<TweenRotationCompleteTag>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            beginSimulationECBSystem.AddJobHandleForProducer(jobHandle);
            endSimulationECSSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
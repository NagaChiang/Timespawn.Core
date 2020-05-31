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

            JobHandle removeCompleteTagJob = Entities.WithAll<TweenRotationCompleteTag>().ForEach((Entity entity, int entityInQueryIndex) =>
            {
                endSimulationCommandBuffer.RemoveComponent<TweenRotationCompleteTag>(entityInQueryIndex, entity);
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(removeCompleteTagJob);

            JobHandle job = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenRotation tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenRotation>(entityInQueryIndex, entity);
                    beginSimulationCommandBuffer.AddComponent(entityInQueryIndex, entity, new TweenRotationCompleteTag());
                }
            }).Schedule(removeCompleteTagJob);

            beginSimulationECBSystem.AddJobHandleForProducer(job);
            endSimulationECSSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
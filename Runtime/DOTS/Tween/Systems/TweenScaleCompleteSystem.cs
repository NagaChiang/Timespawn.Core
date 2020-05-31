using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenScaleCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            BeginSimulationEntityCommandBufferSystem beginSimulationECBSystem = DotsUtils.GetSystemFromDefaultWorld<BeginSimulationEntityCommandBufferSystem>();
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent beginSimulationCommandBuffer = beginSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();

            JobHandle removeCompeteTagJob = Entities.WithAll<TweenScaleCompleteTag>().ForEach((Entity entity, int entityInQueryIndex) =>
            {
                endSimulationCommandBuffer.RemoveComponent<TweenScaleCompleteTag>(entityInQueryIndex, entity);
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(removeCompeteTagJob);

            JobHandle job = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenScaleData tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenScaleData>(entityInQueryIndex, entity);
                    beginSimulationCommandBuffer.AddComponent(entityInQueryIndex, entity, new TweenScaleCompleteTag());
                }
            }).Schedule(removeCompeteTagJob);

            beginSimulationECBSystem.AddJobHandleForProducer(job);
            endSimulationECSSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
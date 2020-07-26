using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenScaleCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().AsParallelWriter();
            JobHandle job = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenScale tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenScale>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            endSimulationECSSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
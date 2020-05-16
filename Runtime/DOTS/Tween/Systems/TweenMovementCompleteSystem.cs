using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenMovementCompleteSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            BeginSimulationEntityCommandBufferSystem beginSimulationECBSystem = DotsUtils.GetSystemFromDefaultWorld<BeginSimulationEntityCommandBufferSystem>();
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.Concurrent beginSimulationCommandBuffer = beginSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenMovementData tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenMovementData>(entityInQueryIndex, entity);
                    beginSimulationCommandBuffer.AddComponent(entityInQueryIndex, entity, new TweenMovementCompleteTag());
                }
            }).Schedule(inputDeps);

            beginSimulationECBSystem.AddJobHandleForProducer(jobHandle);
            endSimulationECSSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
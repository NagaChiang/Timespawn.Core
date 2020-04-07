using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenMovementCompleteSystem : JobComponentSystem
    {
        private BeginSimulationEntityCommandBufferSystem BeginSimulationECBSystem;
        private EndSimulationEntityCommandBufferSystem EndSimulationECSSystem;

        protected override void OnCreate()
        {
            BeginSimulationECBSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            EndSimulationECSSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EntityCommandBuffer.Concurrent beginSimulationCommandBuffer = BeginSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            EntityCommandBuffer.Concurrent endSimulationCommandBuffer = EndSimulationECSSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenMovementData tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    beginSimulationCommandBuffer.AddComponent(entityInQueryIndex, entity, new TweenMovementCompleteTag());
                    endSimulationCommandBuffer.RemoveComponent<TweenMovementData>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            BeginSimulationECBSystem.AddJobHandleForProducer(jobHandle);
            EndSimulationECSSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
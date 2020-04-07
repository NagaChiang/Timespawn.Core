using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenPostCompleteSystemGroup))]
    public class TweenMovementPostCompleteSystem : JobComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem EndSimulationECBSystem;

        protected override void OnCreate()
        {
            EndSimulationECBSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EntityCommandBuffer.Concurrent commandBuffer = EndSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenMovementCompleteTag tag) =>
            {
                commandBuffer.RemoveComponent<TweenMovementCompleteTag>(entityInQueryIndex, entity);
            }).Schedule(inputDeps);

            EndSimulationECBSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
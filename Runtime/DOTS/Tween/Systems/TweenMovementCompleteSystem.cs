using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenMovementCompleteSystem : JobComponentSystem
    {
        private BeginSimulationEntityCommandBufferSystem BeginSimulationECBSystem;

        protected override void OnCreate()
        {
            BeginSimulationECBSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            EntityCommandBuffer.Concurrent commandBuffer = BeginSimulationECBSystem.CreateCommandBuffer().ToConcurrent();
            JobHandle jobHandle = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenMovementData tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    commandBuffer.RemoveComponent<TweenMovementData>(entityInQueryIndex, entity);
                }
            }).Schedule(inputDeps);

            BeginSimulationECBSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}
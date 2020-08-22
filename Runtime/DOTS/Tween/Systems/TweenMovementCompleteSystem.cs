using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenMovementCompleteSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().AsParallelWriter();
            Dependency = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenMovement tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenMovement>(entityInQueryIndex, entity);
                }
            }).Schedule(Dependency);

            endSimulationECSSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
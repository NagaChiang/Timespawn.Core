using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenRotationCompleteSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().AsParallelWriter();
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((Entity entity, int entityInQueryIndex, ref TweenRotation tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenRotation>(entityInQueryIndex, entity);
                }
            }).Schedule(Dependency);

            endSimulationECSSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
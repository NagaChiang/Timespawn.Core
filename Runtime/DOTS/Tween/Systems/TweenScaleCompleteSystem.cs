using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenCompleteSystemGroup))]
    public class TweenScaleCompleteSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimulationECSSystem = DotsUtils.GetSystemFromDefaultWorld<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter endSimulationCommandBuffer = endSimulationECSSystem.CreateCommandBuffer().AsParallelWriter();
            Dependency = Entities.ForEach((Entity entity, int entityInQueryIndex, ref TweenScale tween) =>
            {
                if (TweenSystemUtils.CompleteTweenState(ref tween.State))
                {
                    endSimulationCommandBuffer.RemoveComponent<TweenScale>(entityInQueryIndex, entity);
                }
            }).Schedule(Dependency);

            endSimulationECSSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
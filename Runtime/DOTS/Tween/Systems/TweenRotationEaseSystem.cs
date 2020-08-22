using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenEaseSystemGroup))]
    public class TweenRotationEaseSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((ref TweenRotation tween) =>
            {
                TweenSystemUtils.UpdateTweenState(ref tween.State, deltaTime);
            }).Schedule(Dependency);
        }
    }
}
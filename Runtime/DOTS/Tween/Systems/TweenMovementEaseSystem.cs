using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenEaseSystemGroup))]
    public class TweenMovementEaseSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((ref TweenMovement tween) =>
            {
                TweenSystemUtils.UpdateTweenState(ref tween.State, deltaTime);
            }).Schedule(Dependency);
        }
    }
}

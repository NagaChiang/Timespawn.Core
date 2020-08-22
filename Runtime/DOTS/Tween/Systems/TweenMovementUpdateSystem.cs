using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroup))]
    public class TweenMovementUpdateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((ref Translation translation, in TweenMovement tween) =>
            {
                translation.Value = math.lerp(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(Dependency);
        }
    }
}
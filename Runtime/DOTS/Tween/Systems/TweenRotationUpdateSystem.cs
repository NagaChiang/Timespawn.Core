using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroup))]
    public class TweenRotationUpdateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((ref Rotation rotation, in TweenRotation tween) =>
            {
                rotation.Value = math.slerp(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(Dependency);
        }
    }
}
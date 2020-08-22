using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroup))]
    public class TweenScaleUpdateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Dependency = Entities.WithNone<TweenPauseTag>().ForEach((ref NonUniformScale scale, in TweenScale tween) =>
            {
                scale.Value = math.lerp(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(Dependency);
        }
    }
}
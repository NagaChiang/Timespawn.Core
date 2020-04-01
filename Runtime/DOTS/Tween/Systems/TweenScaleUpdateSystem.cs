using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroups))]
    public class TweenScaleUpdateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            JobHandle jobHandle = Entities.ForEach((ref NonUniformScale scale, in TweenScaleData tween) =>
            {
                scale.Value = Ease.Interpolate(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(inputDeps);
            
            return jobHandle;
        }
    }
}
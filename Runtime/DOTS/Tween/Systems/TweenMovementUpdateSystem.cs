using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroups))]
    public class TweenMovementUpdateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            JobHandle jobHandle = Entities.ForEach((ref Translation translation, in TweenMovementData tween) =>
            {
                translation.Value = Ease.Interpolate(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(inputDeps);
            
            return jobHandle;
        }
    }
}
using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroup))]
    public class TweenScaleUpdateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            JobHandle jobHandle = Entities.ForEach((ref NonUniformScale scale, in TweenScaleData tween) =>
            {
                scale.Value = math.lerp(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(inputDeps);
            
            return jobHandle;
        }
    }
}
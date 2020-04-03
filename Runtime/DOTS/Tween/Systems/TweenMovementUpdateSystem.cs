using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenUpdateSystemGroup))]
    public class TweenMovementUpdateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            JobHandle jobHandle = Entities.ForEach((ref Translation translation, in TweenMovementData tween) =>
            {
                translation.Value = math.lerp(tween.Start, tween.End, tween.State.Percentage);
            }).Schedule(inputDeps);
            
            return jobHandle;
        }
    }
}
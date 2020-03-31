using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    [UpdateInGroup(typeof(TweenEaseSystemGroups))]
    public class TweenMovementEaseSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            JobHandle jobHandle = Entities.ForEach((ref TweenMovementData tween) =>
            {
                TweenSystemUtils.UpdateTweenState(ref tween.State, deltaTime);
            }).Schedule(inputDeps);

            return jobHandle;
        }
    }
}

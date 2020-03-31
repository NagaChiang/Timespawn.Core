using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween.Systems
{
    public class TweenMovementEaseSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            JobHandle jobHandle = Entities.ForEach((ref TweenMovementData tween) =>
            {
                TweenEaseSystemUtils.UpdateTween(ref tween.State, deltaTime);
            }).Schedule(inputDeps);

            return jobHandle;
        }
    }

    public static class TweenEaseSystemUtils
    {
        public static void UpdateTween(ref TweenState state, float deltaTime)
        {
            Assert.IsTrue(state.Duration > 0, "Tween duration should be larger than 0.");

            state.ElapsedTime += deltaTime;
            state.NormalizedTime = math.clamp(state.ElapsedTime / state.Duration, 0.0f, 1.0f);
            state.Percentage = CalculateTweenPercentage(state.NormalizedTime, state.Type);
        }

        private static float CalculateTweenPercentage(float t, EaseType type)
        {
            switch (type)
            {
                case EaseType.Linear:
                    return t;
            }

            return 0.0f;
        }
    }
}

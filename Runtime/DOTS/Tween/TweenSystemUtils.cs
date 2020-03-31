using Timespawn.Core.Math;
using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween
{
    public static class TweenSystemUtils
    {
        public static void UpdateTweenState(ref TweenState state, float deltaTime)
        {
            Assert.IsTrue(state.Duration > 0, "Tween duration should be larger than 0.");

            state.ElapsedTime += deltaTime;
            state.NormalizedTime = math.clamp(state.ElapsedTime / state.Duration, 0.0f, 1.0f);
            state.Percentage = Ease.CalculatePercentage(state.NormalizedTime, state.Type);
        }
    }
}
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

            state.ElapsedTime += state.IsReverting ? -deltaTime : deltaTime;
            state.NormalizedTime = math.clamp(state.ElapsedTime / state.Duration, 0.0f, 1.0f);
            state.Percentage = Ease.CalculatePercentage(state.NormalizedTime, state.Type);
        }

        public static bool CompleteTweenState(ref TweenState state)
        {
            bool isPendingDestroy = false;
            if (state.NormalizedTime >= 1.0f)
            {
                if (state.IsPingPong)
                {
                    state.IsReverting = true;
                }
                else
                {
                    if (state.LoopNum > 0)
                    {
                        state.LoopNum -= 1;
                    }

                    if (state.LoopNum == 0)
                    {
                        isPendingDestroy = true;
                    }
                    else
                    {
                        state.ElapsedTime = 0.0f;
                    }
                }
            }
            else if (state.IsReverting && state.NormalizedTime <= 0.0f)
            {
                if (state.LoopNum > 0)
                {
                    state.LoopNum -= 1;
                }

                if (state.LoopNum == 0)
                {
                    isPendingDestroy = true;
                }
                else
                {
                    state.IsReverting = false;
                }
            }

            return isPendingDestroy;
        }
    }
}
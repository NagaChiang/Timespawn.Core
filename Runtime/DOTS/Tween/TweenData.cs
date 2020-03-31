using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.Core.DOTS.Tween
{
    public struct TweenState
    {
        public EaseType Type;
        public float Duration;
        public float ElapsedTime;
        public float NormalizedTime;
        public float Percentage;

        public TweenState(EaseType type, float duration)
        {
            Type = type;
            Duration = duration;
            ElapsedTime = 0.0f;
            NormalizedTime = 0.0f;
            Percentage = 0.0f;
        }
    }

    public struct TweenMovementData : IComponentData
    {
        public TweenState State;
        public float3 Start;
        public float3 End;
    }
}
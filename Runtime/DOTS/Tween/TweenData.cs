using Timespawn.Core.Math;
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
        public bool IsPingPong;

        public TweenState(EaseType type, float duration, bool isPingPong)
        {
            Type = type;
            Duration = duration;
            ElapsedTime = 0.0f;
            NormalizedTime = 0.0f;
            Percentage = 0.0f;
            IsPingPong = isPingPong;
        }
    }

    public struct TweenMovementData : IComponentData
    {
        public TweenState State;
        public float3 Start;
        public float3 End;
    }

    public struct TweenRotationData : IComponentData
    {
        public TweenState State;
        public quaternion Start;
        public quaternion End;
    }

    public struct TweenScaleData : IComponentData
    {
        public TweenState State;
        public float3 Start;
        public float3 End;
    }
}
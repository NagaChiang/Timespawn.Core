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
        public short LoopNum;
        public bool IsReverting;

        public TweenState(EaseType type, float duration, bool isPingPong, short loopNum)
        {
            Type = type;
            Duration = isPingPong ? duration / 2.0f : duration;
            ElapsedTime = 0.0f;
            NormalizedTime = 0.0f;
            Percentage = 0.0f;
            IsPingPong = isPingPong;
            LoopNum = loopNum;
            IsReverting = false;
        }
    }

    public struct TweenMovement : IComponentData
    {
        public TweenState State;
        public float3 Start;
        public float3 End;
    }

    public struct TweenMovementCompleteTag : IComponentData
    {

    }

    public struct TweenRotation : IComponentData
    {
        public TweenState State;
        public quaternion Start;
        public quaternion End;
    }

    public struct TweenRotationCompleteTag : IComponentData
    {

    }

    public struct TweenScale : IComponentData
    {
        public TweenState State;
        public float3 Start;
        public float3 End;
    }

    public struct TweenScaleCompleteTag : IComponentData
    {

    }

    public struct TweenPauseTag : IComponentData
    {

    }
}
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Timespawn.Core.Math
{
    public enum EaseType : byte
    {
        Linear,
        SmoothStart2,
        SmoothStart3,
        SmoothStart4,
        SmoothStop2,
        SmoothStop3,
        SmoothStop4,
        SmoothStep2,
        SmoothStep3,
        SmoothStep4,
    }

    public static class Ease
    {
        public delegate float EaseFunction(float t);

        public static float Interpolate(float start, float end, float percentage)
        {
            return (start * (1 - percentage)) + (end * percentage);
        }

        public static Vector3 Interpolate(Vector3 start, Vector3 end, float percentage)
        {
            return (start * (1 - percentage)) + (end * percentage);
        }

        public static float SmoothStart(float t, int exponent = 2)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= t;
            }

            return product;
        }

        public static float SmoothStop(float t, int exponent = 2)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= (1 - t);
            }

            return 1 - product;
        }

        public static float SmoothStep(float t, int exponent = 2)
        {
            return Interpolate(SmoothStart(t, exponent), SmoothStop(t, exponent), t);
        }

        public static float CrossFade(EaseFunction easeA, EaseFunction easeB, float t)
        {
            return (easeA(t) * (1 - t)) + (easeB(t) * t);
        }
        
        public static float CalculatePercentage(float t, EaseType type)
        {
            switch (type)
            {
                case EaseType.Linear:
                    return t;
                case EaseType.SmoothStart2:
                    return SmoothStart(t, 2);
                case EaseType.SmoothStart3:
                    return SmoothStart(t, 3);
                case EaseType.SmoothStart4:
                    return SmoothStart(t, 4);
                case EaseType.SmoothStop2:
                    return SmoothStop(t, 2);
                case EaseType.SmoothStop3:
                    return SmoothStop(t, 3);
                case EaseType.SmoothStop4:
                    return SmoothStop(t, 4);
                case EaseType.SmoothStep2:
                    return SmoothStep(t, 2);
                case EaseType.SmoothStep3:
                    return SmoothStep(t, 3);
                case EaseType.SmoothStep4:
                    return SmoothStep(t, 4);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
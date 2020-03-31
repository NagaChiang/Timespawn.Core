using UnityEngine;

namespace Timespawn.Core.Math
{
    public static class Ease
    {
        public delegate float EaseFunction(float t);

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
            return CrossFade(u => SmoothStart(u, exponent), u => SmoothStop(u, exponent), t);
        }

        public static float CrossFade(EaseFunction easeA, EaseFunction easeB, float t)
        {
            return (easeA(t) * (1 - t)) + (easeB(t) * t);
        }
    }
}
using Timespawn.Core.Common;
using Unity.Mathematics;

#if !UNITY_DOTSRUNTIME
using UnityEngine;
#endif

namespace Timespawn.Core.Inputs
{
    public static class InputUtils
    {
#if !UNITY_DOTSRUNTIME
        public static bool TryTranslateInputToDirection(Vector2 delta, out Direction2D outDirection, float deadZone = 1.0f)
        {
            outDirection = default;
            if (delta.sqrMagnitude <= deadZone * deadZone)
            {
                return false;
            }

            if (math.abs(delta.x) > math.abs(delta.y))
            {
                outDirection = delta.x > 0.0f ? Direction2D.Right : Direction2D.Left;
            }
            else
            {
                outDirection = delta.y > 0.0f ? Direction2D.Up : Direction2D.Down;
            }

            return true;
        }
#endif
    }
}
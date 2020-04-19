using Timespawn.Core.Common;
using Unity.Mathematics;
using UnityEngine;

namespace Timespawn.Core.Inputs
{
    public static class InputUtils
    {
        public static bool TryTranslateInputToDirection(Vector2 delta, out Direction outDirection, float deadZone = 1.0f)
        {
            outDirection = default;
            if (delta.sqrMagnitude <= deadZone * deadZone)
            {
                return false;
            }

            if (math.abs(delta.x) > math.abs(delta.y))
            {
                outDirection = delta.x > 0.0f ? Direction.Right : Direction.Left;
            }
            else
            {
                outDirection = delta.y > 0.0f ? Direction.Up : Direction.Down;
            }

            return true;
        }
    }
}
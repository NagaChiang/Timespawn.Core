using Timespawn.Core.Common;
using Unity.Mathematics;
using UnityEngine;

namespace Timespawn.Core.Inputs
{
    public static class InputUtils
    {
        public static Direction Translate4DirectionInput(Vector2 delta, float deadZone = 1.0f)
        {
            if (delta.sqrMagnitude <= deadZone * deadZone)
            {
                return Direction.None;
            }

            if (math.abs(delta.x) > math.abs(delta.y))
            {
                return delta.x > 0.0f ? Direction.Right : Direction.Left;
            }
            else
            {
                return delta.y > 0.0f ? Direction.Up : Direction.Down;
            }
        }
    }
}
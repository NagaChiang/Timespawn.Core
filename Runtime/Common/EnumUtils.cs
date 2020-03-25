using System;
using Unity.Mathematics;

namespace Timespawn.Core.Common
{
    public static class EnumUtils
    {
        public static int2 DirectionToInt2(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new int2(0, 1);
                case Direction.Down:
                    return new int2(0, -1);
                case Direction.Left:
                    return new int2(-1, 0);
                case Direction.Right:
                    return new int2(1, 0);
            }

            return int2.zero;
        }
    }
}
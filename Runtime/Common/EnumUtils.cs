using System;
using Unity.Mathematics;

namespace Timespawn.Core.Common
{
    public static class EnumUtils
    {
        public static int GetEnumCount<T>()
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        public static int2 DirectionToInt2(Direction2D direction)
        {
            switch (direction)
            {
                case Direction2D.Up:
                    return new int2(0, 1);
                case Direction2D.Down:
                    return new int2(0, -1);
                case Direction2D.Left:
                    return new int2(-1, 0);
                case Direction2D.Right:
                    return new int2(1, 0);
            }

            return int2.zero;
        }
    }
}
using System;

namespace Timespawn.Core.Extensions
{
    public static class ArrayExtension
    {
        public static bool IsValidIndex(this Array array, int index)
        {
            return (index >= 0) && (index < array.Length);
        }
    }
}
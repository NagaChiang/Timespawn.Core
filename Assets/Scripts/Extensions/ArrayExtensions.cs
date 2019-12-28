using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsValidIndex(this Array array, int index)
        {
            return (index >= 0) && (index < array.Length);
        }
    }
}
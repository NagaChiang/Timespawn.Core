using Unity.Mathematics;
using UnityEngine;

namespace Timespawn.Core.Extensions
{
    public static class MathExtensions
    {
        public static Vector2 ToVector(this float2 pos)
        {
            return new Vector2(pos.x, pos.y);
        }

        public static Vector3 ToVector(this float3 pos)
        {
            return new Vector3(pos.x, pos.y, pos.z);
        }

        public static Vector4 ToVector(this float4 pos)
        {
            return new Vector4(pos.x, pos.y, pos.z, pos.w);
        }

        public static Vector2Int ToVector(this int2 pos)
        {
            return new Vector2Int(pos.x, pos.y);
        }

        public static Vector3Int ToVector(this int3 pos)
        {
            return new Vector3Int(pos.x, pos.y, pos.z);
        }
    }
}
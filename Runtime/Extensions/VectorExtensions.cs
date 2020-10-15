using Unity.Mathematics;

#if !UNITY_DOTSRUNTIME
using UnityEngine;
#endif

namespace Timespawn.Core.Extensions
{
    public static class VectorExtensions
    {
        public static float3 ToFloat3(this float2 pos)
        {
            return new float3(pos.x, pos.y, 0.0f);
        }

        public static int3 ToInt3(this int2 pos)
        {
            return new int3(pos.x, pos.y, 0);
        }

#if !UNITY_DOTSRUNTIME
        public static Vector3 ToVector3(this Vector2 pos)
        {
            return new Vector3(pos.x, pos.y, 0.0f);
        }

        public static Vector3Int ToVector3Int(this Vector2Int pos)
        {
            return new Vector3Int(pos.x, pos.y, 0);
        }
#endif
    }
}
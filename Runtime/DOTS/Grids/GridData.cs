using System;
using Timespawn.Core.Extensions;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    [GenerateAuthoringComponent]
    public struct GridData : IComponentData
    {
        public UInt16 ColumnNum;
        public UInt16 RowNum;
        public float2 CellSize;

        public float3 GetWorldCellCenter(float3 worldGridCenter, int x, int y)
        {
            return worldGridCenter - GetGridCenter().ToFloat3() + GetCellCenter(x, y).ToFloat3();
        }

        public float3 GetWorldCellCenter(float3 worldGridCenter, int2 coords)
        {
            return GetWorldCellCenter(worldGridCenter, coords.x, coords.y);
        }

        public float2 GetCellCenter(int x, int y)
        {
            Assert.IsTrue(IsValidCoordinates(x, y), "Coordinates should be in range.");

            float2 center = new float2
            {
                x = CellSize.x * (x + 0.5f),
                y = CellSize.y * (y + 0.5f)
            };

            return center;
        }

        public float2 GetCellBottomLeft(int x, int y)
        {
            Assert.IsTrue(IsValidCoordinates(x, y), "Coordinates should be in range.");

            float2 bottomLeft = new float2
            {
                x = x * CellSize.x,
                y = y * CellSize.y
            };

            return bottomLeft;
        }

        public float2 GetGridCenter()
        {
            float2 center = new float2
            {
                x = CellSize.x * ColumnNum * 0.5f,
                y = CellSize.y * RowNum * 0.5f
            };
            
            return center;
        }

        public bool IsValidCoordinates(int x, int y)
        {
            return x >= 0 && x < ColumnNum && y >= 0 && y < RowNum;
        }

        public bool IsValidCoordinates(int2 coords)
        {
            return IsValidCoordinates(coords.x, coords.y);
        }
    }
}
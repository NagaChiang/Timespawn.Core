using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    public struct GridData<TCellData> : IComponentData where TCellData : unmanaged
    {
        public BlobAssetReference<GridDataBlob<TCellData>> Blob;

        public float2 GetCellCenter(int x, int y)
        {
            Assert.IsTrue(IsValidCoordinates(x, y));

            float2 cellSize = Blob.Value.CellSize;
            
            float2 center = float2.zero;
            center.x = cellSize.x * (x + 0.5f);
            center.y = cellSize.y * (y + 0.5f);

            return center;
        }

        public float2 GetCellBottomLeft(int x, int y)
        {
            Assert.IsTrue(IsValidCoordinates(x, y));

            float2 cellSize = Blob.Value.CellSize;

            float2 bottomLeft = float2.zero;
            bottomLeft.x = x * cellSize.x;
            bottomLeft.y = y * cellSize.y;

            return bottomLeft;
        }

        public TCellData GetCell(int x, int y)
        {
            Assert.IsTrue(IsValidCoordinates(x, y));

            int index = y * Blob.Value.ColumnNum + x;
            return Blob.Value.CellDataArray[index];
        }

        public float2 GetGridCenter()
        {
            float2 cellSize = Blob.Value.CellSize;
            
            float2 center = float2.zero;
            center.x = cellSize.x * Blob.Value.ColumnNum * 0.5f;
            center.y = cellSize.y * Blob.Value.RowNum * 0.5f;

            return center;
        }

        public bool IsValidCoordinates(int x, int y)
        {
            return x >= 0 && x < Blob.Value.ColumnNum && y >= 0 && y < Blob.Value.RowNum;
        }
    }

    public struct GridDataBlob<TCellData> where TCellData : unmanaged
    {
        public ushort ColumnNum;
        public ushort RowNum;
        public float2 CellSize;
        public BlobArray<TCellData> CellDataArray;
    }
}
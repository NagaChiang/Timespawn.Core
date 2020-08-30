using Unity.Mathematics;
using UnityEngine.Assertions;

namespace Timespawn.Core.Collections
{
    public class GridMap<TCellType>
    {
        public int ColumnNum { get; protected set; }
        public int RowNum { get; protected set; }
        public float2 CellSize { get; protected set; }
        public float2 Origin { get; protected set; }

        protected TCellType[,] Cells;

        public GridMap(int columnNum, int rowNum, float2 cellSize, float2 origin = new float2())
        {
            ColumnNum = columnNum;
            RowNum = rowNum;
            CellSize = cellSize;
            Origin = origin;
            Cells = new TCellType[ColumnNum, RowNum];
        }

        public TCellType GetCell(int x, int y)
        {
            AssertValidIndex(x, y);
            return Cells[x, y];
        }

        public void SetCell(int x, int y, TCellType newCell)
        {
            AssertValidIndex(x, y);
            Cells[x, y] = newCell;
        }

        public float2 GetCellCenter(int x, int y)
        {
            AssertValidIndex(x, y);
            return Origin + new float2((x + 0.5f) * CellSize.x, (y + 0.5f) * CellSize.y);
        }

        public float2 GetCenter()
        {
            return Origin + GetSize() * 0.5f;
        }

        public void SetCenter(float2 center)
        {
            Origin = center - GetSize() * 0.5f;
        }

        public void Fill(TCellType cell)
        {
            for (int y = 0; y < RowNum; y++)
            {
                for (int x = 0; x < ColumnNum; x++)
                {
                    Cells[x, y] = cell;
                }
            }
        }

        public void SetOrigin(float2 origin)
        {
            Origin = origin;
        }

        public float2 GetSize()
        {
            return new float2(GetWidth(), GetHeight());
        }

        public float GetWidth()
        {
            return CellSize.x * ColumnNum;
        }

        public float GetHeight()
        {
            return CellSize.y * RowNum;
        }

        protected void AssertValidIndex(int x, int y)
        {
            Assert.IsTrue(x >= 0 && x < ColumnNum, "x should be within bounds.");
            Assert.IsTrue(y >= 0 && y < RowNum, "y should be within bounds.");
        }
    }
}


using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.Core.DOTS.Grids
{
    public struct CellData : IComponentData
    {
        public UInt16 x;
        public UInt16 y;

        public int2 GetCoords()
        {
            return new int2(x, y);
        }
    }
}
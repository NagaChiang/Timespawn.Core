using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.Core.DOTS.Grids
{
    public struct CellData : IComponentData
    {
        public ushort x;
        public ushort y;

        public int2 GetCoord()
        {
            return new int2(x, y);
        }
    }
}
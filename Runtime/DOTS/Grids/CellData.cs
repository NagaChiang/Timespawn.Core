using System;
using Unity.Entities;

namespace Timespawn.Core.DOTS.Grids
{
    public struct CellData : IComponentData
    {
        public UInt16 x;
        public UInt16 y;
    }
}
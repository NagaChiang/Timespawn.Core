using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.Core.DOTS.Grids
{
    [GenerateAuthoringComponent]
    public struct GridGenerationData : IComponentData
    {
        public ushort ColumnNum;
        public ushort RowNum;
        public float2 CellSize;
    }
}
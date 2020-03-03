using Timespawn.Core.DOTS.Grids;
using Timespawn.Core.Extensions;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Timespawn.Core.Editor.DOTS.Grids
{
    [AlwaysSynchronizeSystem]
    public class GridDebugDrawSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities.ForEach((in Translation translation, in GridData gridData) =>
            {
                float2 cellSize = gridData.CellSize;
                Vector3 worldGridButtonLeft = gridData.CalculateWorldBottonLeft(translation.Value);
                for (int y = 0; y < gridData.RowNum + 1; y++)
                {
                    for (int x = 0; x < gridData.ColumnNum + 1; x++)
                    {
                        Vector3 start = worldGridButtonLeft + new Vector3(x * cellSize.x, y * cellSize.y, 0.0f);

                        if (x + 1 <= gridData.ColumnNum)
                        {
                            Debug.DrawLine(start, start + cellSize.x * Vector3.right);
                        }

                        if (y + 1 <= gridData.RowNum)
                        {
                            Debug.DrawLine(start, start + cellSize.y * Vector3.up);
                        }
                    }
                }
            }).Run();

            return default;
        }
    }
}
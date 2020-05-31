using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    public static class GridUtils
    {
        public static Entity CreateCellEntity(float3 gridCenter, Grid grid, UInt16 x, UInt16 y, Entity prefab)
        {
            Assert.IsTrue(grid.IsValidCoord(x, y), "Should be valid coordinates in the grid.");
            Assert.IsTrue(prefab != Entity.Null, "Should provide a non-null entity prefab.");

            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            Entity entity = commandBuffer.Instantiate(prefab);
            commandBuffer.SetComponent(entity, new Translation
            {
                Value = grid.GetWorldCellCenter(gridCenter, x, y),
            });
            
            commandBuffer.AddComponent(entity, new Cell
            {
                x = x,
                y = y,
            });

            return entity;
        }

        public static bool IsCellEmpty(NativeArray<Cell> cells, int2 coord)
        {
            foreach (Cell cell in cells)
            {
                if (cell.x == coord.x && cell.y == coord.y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
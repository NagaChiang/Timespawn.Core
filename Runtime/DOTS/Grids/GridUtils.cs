using System;
using Timespawn.Core.Extensions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    public static class GridUtils
    {
        public static Entity CreateCellEntity(EntityCommandBuffer commandBuffer, float3 gridCenter, GridData gridData, UInt16 x, UInt16 y, Entity prefab)
        {
            Assert.IsTrue(gridData.IsValidCoordinates(x, y), "Should be valid coordinates in the grid.");
            Assert.IsTrue(prefab != Entity.Null, "Should provide a non-null entity prefab.");

            Entity entity = commandBuffer.Instantiate(prefab);
            float3 worldGridButtonLeft = gridCenter - gridData.GetGridCenter().ToFloat3();
            float3 worldCellCenter = worldGridButtonLeft + gridData.GetCellCenter(x, y).ToFloat3();
            commandBuffer.SetComponent(entity, new Translation
            {
                Value = worldCellCenter,
            });
            
            commandBuffer.AddComponent(entity, new CellData
            {
                x = x,
                y = y,
            });

            return entity;
        }
    }
}
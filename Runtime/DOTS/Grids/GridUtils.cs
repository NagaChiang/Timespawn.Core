using System;
using Timespawn.Core.Extensions;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    public static class GridUtils
    {
        public static Entity CreateCellEntity(EntityManager entityManager, float3 gridCenter, GridData gridData, UInt16 x, UInt16 y, Entity prefab)
        {
            Assert.IsTrue(gridData.IsValidCoordinates(x, y), "Should be valid coordinates in the grid.");
            Assert.IsTrue(prefab != Entity.Null, "Should provide a non-null entity prefab.");

            Entity entity = entityManager.Instantiate(prefab);
            entityManager.SetComponentData(entity, new Translation
            {
                Value = gridData.GetWorldCellCenter(gridCenter, x, y),
            });
            
            entityManager.AddComponentData(entity, new CellData
            {
                x = x,
                y = y,
            });

            return entity;
        }

        public static void MoveCellTo(EntityManager entityManager, Entity entity, float3 gridCenter, GridData gridData, int2 destCoords)
        {
            Assert.IsTrue(gridData.IsValidCoordinates(destCoords), "Should be valid coordinates in the grid.");

            entityManager.SetComponentData(entity, new Translation
            {
                Value = gridData.GetWorldCellCenter(gridCenter, destCoords),
            });
            
            entityManager.SetComponentData(entity, new CellData
            {
                x = (ushort) destCoords.x,
                y = (ushort) destCoords.y,
            });
        }

        public static bool IsCellEmpty(NativeArray<CellData> cells, int2 coords)
        {
            foreach (CellData cell in cells)
            {
                if (cell.x == coords.x && cell.y == coords.y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
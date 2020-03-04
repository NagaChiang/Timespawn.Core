using System;
using Timespawn.Core.Extensions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Grids
{
    public static class GridUtils
    {
        public static Entity CreateCellEntity(float3 gridCenter, GridData gridData, UInt16 x, UInt16 y, Entity entityPrefab)
        {
            Assert.IsTrue(gridData.IsValidCoordinates(x, y), "Should be valid coordinates in the grid.");
            Assert.IsTrue(entityPrefab != Entity.Null, "Should provide a entity prefab.");

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity entity = entityManager.Instantiate(entityPrefab);

            float3 worldGridButtonLeft = gridCenter - gridData.GetGridCenter().ToFloat3();
            float3 worldCellCenter = worldGridButtonLeft + gridData.GetCellCenter(x, y).ToFloat3();
            entityManager.SetComponentData(entity, new Translation
            {
                Value = worldCellCenter,
            });

            entityManager.AddComponentData(entity, new CellData
            {
                x = x,
                y = y,
            });

            return entity;
        }
    }
}
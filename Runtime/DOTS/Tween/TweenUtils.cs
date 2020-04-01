using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween
{
    public static class TweenUtils
    {
        public static void MoveEntity(Entity entity, float duration, float3 start, float3 end, EaseType type = EaseType.Linear)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponentData(entity, new TweenMovementData
            {
                State = new TweenState(type, duration),
                Start = start,
                End = end,
            });
        }

        public static void MoveEntity(EntityCommandBuffer commandBuffer, Entity entity, float duration, float3 start, float3 end, EaseType type = EaseType.Linear)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            commandBuffer.AddComponent(entity, new TweenMovementData
            {
                State = new TweenState(type, duration),
                Start = start,
                End = end,
            });
        }

        public static void ScaleEntity(Entity entity, float duration, float3 start, float3 end, EaseType type = EaseType.Linear)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponentData(entity, new TweenScaleData
            {
                State = new TweenState(type, duration),
                Start = start,
                End = end,
            });

            if (!entityManager.HasComponent(entity, typeof(NonUniformScale)))
            {
                entityManager.AddComponentData(entity, new NonUniformScale
                {
                    Value = new float3(1.0f),
                });
            }
        }

        public static void ScaleEntity(EntityCommandBuffer commandBuffer, Entity entity, float duration, float3 start, float3 end, EaseType type = EaseType.Linear)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            commandBuffer.AddComponent(entity, new TweenScaleData
            {
                State = new TweenState(type, duration),
                Start = start,
                End = end,
            });

            if (!entityManager.HasComponent(entity, typeof(NonUniformScale)))
            {
                commandBuffer.AddComponent(entity, new NonUniformScale
                {
                    Value = new float3(1.0f),
                });
            }
        }
    }
}
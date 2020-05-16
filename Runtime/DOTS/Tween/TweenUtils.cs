using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween
{
    public static class TweenUtils
    {
        public static void MoveEntity(
            Entity entity,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.AddComponent(entity, new TweenMovementData
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity);
        }

        public static void RotateEntity(
            Entity entity,
            float duration,
            quaternion start,
            quaternion end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.AddComponent(entity, new TweenRotationData
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity);
        }

        public static void ScaleEntity(
            Entity entity,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.AddComponent(entity, new TweenScaleData
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
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

            ResumeEntity(entity);
        }

        public static void PauseEntity(Entity entity)
        {
            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.AddComponent(entity, new TweenPauseTag());
        }

        public static void ResumeEntity(Entity entity)
        {
            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.RemoveComponent<TweenPauseTag>(entity);
        }

        public static void StopEntity(Entity entity)
        {
            EntityCommandBuffer commandBuffer = DotsUtils.CreateECBFromSystem<BeginSimulationEntityCommandBufferSystem>();
            commandBuffer.RemoveComponent<TweenMovementData>(entity);
            commandBuffer.RemoveComponent<TweenRotationData>(entity);
            commandBuffer.RemoveComponent<TweenScaleData>(entity);
        }
    }
}
using Timespawn.Core.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Assertions;

namespace Timespawn.Core.DOTS.Tween
{
    public static class TweenUtils
    {
        public static readonly ComponentType[] ReadOnlyTweenComponentTypes = new ComponentType[]
        {
            ComponentType.ReadOnly<TweenMovement>(), 
            ComponentType.ReadOnly<TweenRotation>(), 
            ComponentType.ReadOnly<TweenScale>(), 
        };

        public static void MoveEntity(
            Entity entity,
            EntityCommandBuffer commandBuffer,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            commandBuffer.AddComponent(entity, new TweenMovement
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity, commandBuffer);
        }

        public static void MoveEntity(
            Entity entity,
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            parallelWriter.AddComponent(sortKey, entity, new TweenMovement
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity, parallelWriter, sortKey);
        }

        public static void RotateEntity(
            Entity entity,
            EntityCommandBuffer commandBuffer,
            float duration,
            quaternion start,
            quaternion end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            commandBuffer.AddComponent(entity, new TweenRotation
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity, commandBuffer);
        }

        public static void RotateEntity(
            Entity entity,
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            float duration,
            quaternion start,
            quaternion end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");
            
            parallelWriter.AddComponent(sortKey, entity, new TweenRotation
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            ResumeEntity(entity, parallelWriter, sortKey);
        }

        public static void ScaleEntity(
            Entity entity,
            EntityCommandBuffer commandBuffer,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            commandBuffer.AddComponent(entity, new TweenScale
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            commandBuffer.AddComponent(entity, new NonUniformScale
            {
                Value = new float3(1.0f),
            });

            ResumeEntity(entity, commandBuffer);
        }

        public static void ScaleEntity(
            Entity entity,
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            float duration,
            float3 start,
            float3 end,
            EaseType type = EaseType.Linear,
            bool isPingPong = false,
            short loopNum = 1)
        {
            Assert.IsTrue(duration > 0, "Tween duration should be larger than 0.");

            parallelWriter.AddComponent(sortKey, entity, new TweenScale
            {
                State = new TweenState(type, duration, isPingPong, loopNum),
                Start = start,
                End = end,
            });

            parallelWriter.AddComponent(sortKey, entity, new NonUniformScale
            {
                Value = new float3(1.0f),
            });

            ResumeEntity(entity, parallelWriter, sortKey);
        }

        public static void PauseEntity(Entity entity, EntityCommandBuffer commandBuffer)
        {
            commandBuffer.AddComponent(entity, new TweenPauseTag());
        }

        public static void PauseEntity(Entity entity, EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey)
        {
            parallelWriter.AddComponent(sortKey, entity, new TweenPauseTag());
        }

        public static void ResumeEntity(Entity entity, EntityCommandBuffer commandBuffer)
        {
            commandBuffer.RemoveComponent<TweenPauseTag>(entity);
        }

        public static void ResumeEntity(Entity entity, EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey)
        {
            parallelWriter.RemoveComponent<TweenPauseTag>(sortKey, entity);
        }

        public static void StopEntity(Entity entity, EntityCommandBuffer commandBuffer)
        {
            commandBuffer.RemoveComponent<TweenMovement>(entity);
            commandBuffer.RemoveComponent<TweenRotation>(entity);
            commandBuffer.RemoveComponent<TweenScale>(entity);
        }

        public static void StopEntity(Entity entity, EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey)
        {
            parallelWriter.RemoveComponent<TweenMovement>(sortKey, entity);
            parallelWriter.RemoveComponent<TweenRotation>(sortKey, entity);
            parallelWriter.RemoveComponent<TweenScale>(sortKey, entity);
        }
    }
}
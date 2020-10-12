using Unity.Entities;

namespace Timespawn.Core.DOTS
{
    public static class DotsUtils
    {
        public static T GetSystemFromDefaultWorld<T>() where T : ComponentSystemBase
        {
            return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<T>();
        }

        public static EntityCommandBuffer CreateCommandBuffer<T>() where T : EntityCommandBufferSystem
        {
            return GetSystemFromDefaultWorld<T>().CreateCommandBuffer();
        }

        public static EntityCommandBuffer.ParallelWriter CreateParallelWriter<T>() where T : EntityCommandBufferSystem
        {
            return GetSystemFromDefaultWorld<T>().CreateCommandBuffer().AsParallelWriter();
        }
    }
}
using Unity.Entities;

namespace Timespawn.Core.DOTS
{
    public static class DotsUtils
    {
        public static T GetSystemFromDefaultWorld<T>() where T : ComponentSystemBase
        {
            return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<T>();
        }

        public static EntityCommandBuffer CreateECBFromSystem<T>() where T : EntityCommandBufferSystem
        {
            return GetSystemFromDefaultWorld<T>().CreateCommandBuffer();
        }
    }
}
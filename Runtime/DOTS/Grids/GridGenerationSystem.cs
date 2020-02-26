using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Timespawn.Core.DOTS.Grids
{
    [AlwaysSynchronizeSystem]
    public abstract class GridGenerationSystem<TCellData> : JobComponentSystem where TCellData : unmanaged
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            /*
            Entities.ForEach((Entity entity, ref GridGenerationData gridGenerationData) =>
            {
                BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp);
                ref GridDataBlob<TCellData> gridDataBlob = ref blobBuilder.ConstructRoot<GridDataBlob<TCellData>>();
                blobBuilder.Dispose();   
            }).Run();
            */

            return default;
        }
    }
}
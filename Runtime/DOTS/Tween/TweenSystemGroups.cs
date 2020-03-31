using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class TweenSystemGroups : ComponentSystemGroup
    {

    }

    [UpdateInGroup(typeof(TweenSystemGroups))]
    public class TweenEaseSystemGroups : ComponentSystemGroup
    {

    }

    [UpdateInGroup(typeof(TweenSystemGroups))]
    [UpdateAfter(typeof(TweenEaseSystemGroups))]
    public class TweenUpdateSystemGroups : ComponentSystemGroup
    {

    }
}
using Unity.Entities;

namespace Timespawn.Core.DOTS.Tween
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class TweenSystemGroup : ComponentSystemGroup
    {

    }

    [UpdateInGroup(typeof(TweenSystemGroup))]
    public class TweenEaseSystemGroup : ComponentSystemGroup
    {

    }

    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenEaseSystemGroup))]
    public class TweenUpdateSystemGroup : ComponentSystemGroup
    {

    }

    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenUpdateSystemGroup))]
    public class TweenCompleteSystemGroup : ComponentSystemGroup
    {

    }
}
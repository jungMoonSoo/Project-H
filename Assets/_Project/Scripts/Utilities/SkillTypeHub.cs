using System;

public static class SkillTypeHub
{
    private readonly static SingleTargetingSystem singleTargetingSystem = new();
    private readonly static LinearTargetingSystem linearTargetingSystem = new();
    private readonly static AreaTargetingSystem areaTargetingSystem = new();
    private readonly static ArcTargetingSystem arcTargetingSystem = new();

    public static IRangeTargeting GetTargetingSystem(RangeType type)
    {
        return type switch
        {
            RangeType.Single => singleTargetingSystem,
            RangeType.Linear => linearTargetingSystem,
            RangeType.Area => areaTargetingSystem,
            RangeType.Arc => arcTargetingSystem,
            _ => throw new Exception("TargetingSystem Type 미존재."),
        };
    }
}

using System;

public static class SkillTypeHub
{
    private readonly static SingleTargetingSystem singleTargetingSystem = new();
    private readonly static LinearTargetingSystem linearTargetingSystem = new();
    private readonly static AreaTargetingSystem areaTargetingSystem = new();

    public static IRangeTargeting GetTargetingSystem(RangeType type)
    {
        return type switch
        {
            RangeType.Single => singleTargetingSystem,
            RangeType.Linear => linearTargetingSystem,
            RangeType.Area => areaTargetingSystem,
            _ => throw new Exception("TargetingSystem Type 미존재."),
        };
    }
}

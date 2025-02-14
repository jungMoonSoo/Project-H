using System;

public static class SkillTypeHub
{
    private readonly static SingleTargetingSystem singleTargetingSystem = new();
    private readonly static LinearTargetingSystem linearTargetingSystem = new();
    private readonly static AreaTargetingSystem areaTargetingSystem = new();

    public static ITargetingSystem GetTargetingSystem(TargetingSystemType type)
    {
        return type switch
        {
            TargetingSystemType.Single => singleTargetingSystem,
            TargetingSystemType.Linear => linearTargetingSystem,
            TargetingSystemType.Area => areaTargetingSystem,
            _ => throw new Exception("TargetingSystem Type 미존재."),
        };
    }
}

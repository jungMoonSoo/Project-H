using System;

public static class SkillTypeHub
{
    private readonly static SingleTargetingSystem singleTargetingSystem = new();
    private readonly static LinearTargetingSystem linearTargetingSystem = new();
    private readonly static AreaTargetingSystem areaTargetingSystem = new();

    private readonly static SingleSkillArea singleSkillArea = new();
    private readonly static LinearSkillArea linearSkillArea = new();
    private readonly static EllipseSkillArea ellipseSkillArea = new();

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

    public static ISkillArea GetSkillArea(SkillAreaType type)
    {
        return type switch
        {
            SkillAreaType.Single => singleSkillArea,
            SkillAreaType.Linear => linearSkillArea,
            SkillAreaType.Area => ellipseSkillArea,
            _ => throw new Exception("SkillArea Type 미존재."),
        };
    }
}

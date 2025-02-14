using System;

public static class SkillAreaHub
{
    private static readonly MeSkillArea meSkillArea = new();
    private static readonly SingleSkillArea allySingleSkillArea = new(SingleSkillArea.SingleTargetType.Ally);
    private static readonly SingleSkillArea weSingleSkillArea = new(SingleSkillArea.SingleTargetType.We);
    private static readonly SingleSkillArea theySingleSkillArea = new(SingleSkillArea.SingleTargetType.They);
    private static readonly GroundSkillArea groundSkillArea = new();
    private static readonly RotateSkillArea rotateSkillArea = new();

    
    public static ISkillArea GetSkillArea(SkillAreaType type)
    {
        return type switch
        {
            SkillAreaType.Me => meSkillArea,
            SkillAreaType.AllySingle => allySingleSkillArea,
            SkillAreaType.WeSingle => weSingleSkillArea,
            SkillAreaType.TheySingle => theySingleSkillArea,
            SkillAreaType.Ground => groundSkillArea,
            SkillAreaType.Rotate => rotateSkillArea,
            _ => throw new Exception("SkillArea Type 미존재."),
        };
    }
}
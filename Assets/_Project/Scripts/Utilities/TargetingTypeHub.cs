using System;

public static class TargetingTypeHub
{
    private readonly static NearUniTargeting nearFirstTarget = new();

    public static IUnidadTargeting GetTargetingSystem(UnidadTargetingType type)
    {
        return type switch
        {
            UnidadTargetingType.Near => nearFirstTarget,
            _ => throw new Exception("UnidadTargeting Type 미존재."),
        };
    }
}

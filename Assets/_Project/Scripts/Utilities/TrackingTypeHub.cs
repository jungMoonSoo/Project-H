using System;

public static class TrackingTypeHub
{
    private readonly static NearUniTargeting nearFirstTarget = new();

    public static ITrackingSystem GetSystem(TrackingType type)
    {
        return type switch
        {
            TrackingType.Near => nearFirstTarget,
            _ => throw new Exception("UnidadTargeting Type 미존재."),
        };
    }
}

using System;

public static class TargetingFilterHub
{
    private readonly static AllTargetingFilter allTargetingFilter = new();
    private readonly static SingleTargetingFilter singleTargetingFilter = new();

    public static ITargetingFilter GetFilter(FilterType type)
    {
        return type switch
        {
            FilterType.All => allTargetingFilter,
            FilterType.Single => singleTargetingFilter,
            _ => throw new Exception("TargetingFilter Type 미존재."),
        };
    }
}

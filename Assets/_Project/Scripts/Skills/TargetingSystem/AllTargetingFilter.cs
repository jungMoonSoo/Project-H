using System.Collections.Generic;
using UnityEngine;

public class AllTargetingFilter : ITargetingFilter
{
    public Unidad[] GetFilteredTargets(List<Unidad> unidads, Vector3 castedPosition)
    {
        return unidads.ToArray();
    }
}
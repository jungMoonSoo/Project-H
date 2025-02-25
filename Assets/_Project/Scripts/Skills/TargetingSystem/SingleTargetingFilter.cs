using System.Collections.Generic;
using UnityEngine;

public class SingleTargetingFilter : ITargetingFilter
{
    public Unidad[] GetFilteredTargets(List<Unidad> unidads, Vector3 castedPosition)
    {
        Unidad target = null;
        float minRange = float.MaxValue;
            
        foreach (Unidad enemy in unidads)
        {
            Vector3 dir = enemy.transform.position - castedPosition;
            float range = dir.magnitude;
            
            if (range < minRange)
            {
                minRange = range;
                target = enemy;
            }
        }
        
        return new[] { target };
    }
}
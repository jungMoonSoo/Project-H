using System.Linq;
using UnityEngine;

public class SingleTargetingSystem: ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        Unidad[] targets = UnidadManager.Instance.GetUnidads(targetOwner, targetType).ToArray();
        Unidad target = null;
        float minRange = float.MaxValue;
            
        foreach (Unidad enemy in targets)
        {
            Vector2 dir = (Vector2)enemy.transform.position - castedPosition;
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
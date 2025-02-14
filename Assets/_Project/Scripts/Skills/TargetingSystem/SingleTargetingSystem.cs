using UnityEngine;

public class SingleTargetingSystem: IRangeTargeting
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize)
    {
        Unidad[] targets = UnidadManager.Instance.GetUnidads(targetOwner, targetType).ToArray();
        Unidad target = null;
        float minRange = float.MaxValue;
            
        foreach (Unidad enemy in targets)
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
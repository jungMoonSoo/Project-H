using System.Linq;
using UnityEngine;

public class SingleTargetingSystem: ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, Vector3 worldPosition, Vector3 rangeSize)
    {
        Unidad[] targets = UnidadManager.Instance.unidades.Where(x => x.Owner != UnitType.Ally).ToArray();
        Unidad target = null;
        float minRange = float.MaxValue;
            
        foreach (Unidad enemy in targets)
        {
            Vector2 dir = enemy.transform.position - worldPosition;
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
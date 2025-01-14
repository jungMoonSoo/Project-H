using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaTargetingSystem: ITargetingSystem
{
    private readonly List<Unidad> unidads = new();
    
    
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        unidads.Clear();
        Unidad[] targets = UnidadManager.Instance.GetUnidads(targetOwner, targetType).ToArray();
        foreach (Unidad unidad in targets)
        {
            if (VectorCalc.CalcEllipse(castedPosition, unidad.transform.position, rangeSize, unidad.unitCollider.size)
                <= 1)
            {
                unidads.Add(unidad);
            }
        }
        
        return unidads.ToArray();
    }
}
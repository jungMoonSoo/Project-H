using System.Collections.Generic;
using UnityEngine;

public class AreaTargetingSystem: IRangeTargeting
{
    private readonly List<Unidad> unidads = new();
    
    
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize)
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
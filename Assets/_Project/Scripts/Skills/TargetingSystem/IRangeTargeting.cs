using UnityEngine;

public interface IRangeTargeting
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize);
}
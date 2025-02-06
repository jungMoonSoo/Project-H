using UnityEngine;

public interface ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize);
}
using UnityEngine;

public interface ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize);
}
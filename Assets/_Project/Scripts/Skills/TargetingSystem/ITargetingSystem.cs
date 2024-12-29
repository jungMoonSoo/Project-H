using UnityEngine;

public interface ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize);
}
using UnityEngine;

public interface ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, Vector3 worldPosition, Vector3 rangeSize);
}
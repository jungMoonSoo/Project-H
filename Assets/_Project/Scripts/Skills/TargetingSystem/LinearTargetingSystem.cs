using UnityEngine;

public class LinearTargetingSystem: ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        // TODO
        //  직사각형으로 Target을 지정해주는 작업 필요
        return new Unidad[] { null };
    }
}
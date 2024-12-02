using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateBase
{
    protected Unit unit;

    public UnitStateBase(Unit _unit)
    {
        unit = _unit;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

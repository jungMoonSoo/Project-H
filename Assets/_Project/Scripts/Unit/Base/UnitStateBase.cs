using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateBase
{
    protected Unit unit;

    protected Unit target;
    protected Vector3 movePos;

    protected Unit onTarget;
    protected bool onEllipse;

    public UnitStateBase(Unit _unit, UnitStateBase _base)
    {
        unit = _unit;

        if (_base == null) return;

        target = _base.target;
        movePos = _base.movePos;

        onTarget = _base.onTarget;
        onEllipse = _base.onEllipse;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

#if UNITY_EDITOR
    public Vector3 GetMovePos() => movePos;
#endif
}

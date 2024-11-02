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

    protected void Flip(float _target, float _this)
    {
        if (_target < _this) unit.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (_target > _this) unit.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

#if UNITY_EDITOR
    public Vector3 GetMovePos()
    {
        return movePos;
    }
#endif
}

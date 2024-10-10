using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateBase
{
    protected Unit unit;

    protected Unit target;
    protected Unit onTarget;

    protected Vector3 movePos;

    protected float checkDist;
    protected float checkClosetDist;

    public UnitStateBase(Unit _unit)
    {
        unit = _unit;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public void SetTarget(bool _isAlly)
    {
        if (target == null) checkClosetDist = float.MaxValue;
        else checkClosetDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Unit, EllipseType.Unit);

        if (checkClosetDist <= 1) return;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != unit && UnitManager.Instance.units[i].isAlly == !_isAlly)
            {
                checkDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Attack, EllipseType.Unit);

                if (checkDist < checkClosetDist)
                {
                    checkClosetDist = checkDist;

                    target = UnitManager.Instance.units[i];
                }
            }
        }

        if (target == null) unit.StateChange(UnitState.Idle);
    }

    protected bool OnEllipseEnter()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) movePos = Vector3.zero;
        else movePos = unit.EllipseCollider.TransAreaPos(unit.status.moveSpeed.Data * (target.transform.position - unit.transform.position).normalized);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != unit && unit.EllipseCollider.OnEllipseEnter(unit.transform.position + movePos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1)
            {
                onTarget = UnitManager.Instance.units[i];
                movePos = unit.EllipseCollider.AroundTarget(movePos, UnitManager.Instance.units[i].EllipseCollider, unit.status.moveSpeed.Data);

                unit.StateChange(UnitState.Move);

                return true;
            }
        }

        movePos = unit.EllipseCollider.TransAreaPos(movePos);

        return false;
    }

#if UNITY_EDITOR
    public Vector3 GetMovePos()
    {
        return movePos;
    }
#endif
}

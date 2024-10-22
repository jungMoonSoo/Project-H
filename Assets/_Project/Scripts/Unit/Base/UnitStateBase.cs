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

    public void SetTarget()
    {
        target = null;

        if (!UnitManager.Instance.isPlay) return;

        if (target == null) checkClosetDist = float.MaxValue;
        else checkClosetDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Unit, EllipseType.Unit);

        if (checkClosetDist <= 1) return;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i].isAlly == !unit.isAlly)
            {
                checkDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Attack, EllipseType.Unit);

                if (checkDist < checkClosetDist)
                {
                    checkClosetDist = checkDist;

                    target = UnitManager.Instance.units[i];
                }
            }
        }

        if (target == null) UnitManager.Instance.End();
    }

    protected bool OnEllipseEnter()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) movePos = Vector3.zero;
        else movePos = GetMoveVector(target.transform, unit.transform);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != unit && unit.EllipseCollider.OnEllipseEnter(unit.transform.position + movePos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1)
            {
                onTarget = UnitManager.Instance.units[i];
                movePos = unit.EllipseCollider.AroundTarget(movePos, onTarget.EllipseCollider, unit.status.moveSpeed.Data);

                unit.StateChange(UnitState.Move);

                return true;
            }
        }

        movePos = unit.EllipseCollider.TransAreaPos(movePos);

        return false;
    }

    protected Vector3 GetMoveVector(Transform _from, Transform _to)
    {
        return unit.status.moveSpeed.Data * (_from.position - _to.position).normalized;
    }

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

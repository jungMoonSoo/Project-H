using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitStateBase
{
    private float checkDist;
    private float checkClosetDist;

    public UnitState_Idle(Unit _unit, UnitStateBase _base) : base(_unit, _base)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        SetTarget();

        if (target == null)
        {
            unit.Animator.Play("Idle_" + unit.StateNum);

            return;
        }

        if (!UnitManager.Instance.isPlay) return;

        if (unit.CheckSkill())
        {
            unit.Status.mp[0].Data = 0;
            unit.StateChange(UnitState.Skill);

            return;
        }

        if (onEllipse)
        {
            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, onTarget.EllipseCollider, EllipseType.Unit, EllipseType.Unit) > 1.5f) onEllipse = false;
            else movePos = unit.EllipseCollider.TransAreaPos(movePos + GetMoveVector(unit.transform, onTarget.transform));
        }
        else
        {
            onEllipse = OnEllipseEnter();

            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1)
            {
                if (unit.notMove) unit.StateChange(UnitState.Attack);
                else if (!onEllipse) unit.StateChange(UnitState.Attack);

                return;
            }

            Flip(movePos.x, 0);
        }

        if (unit.notMove)
        {
            unit.Animator.Play("Idle_" + unit.StateNum);

            return;
        }
        else unit.StateChange(UnitState.Move);
    }

    public override void OnExit()
    {

    }

    private void SetTarget()
    {
        target = null;

        if (!UnitManager.Instance.isPlay) return;

        checkClosetDist = float.MaxValue;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i].isAlly != unit.isAlly)
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

    private bool OnEllipseEnter()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) movePos = Vector3.zero;
        else movePos = GetMoveVector(target.transform, unit.transform);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != unit && unit.EllipseCollider.OnEllipseEnter(unit.transform.position + movePos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1)
            {
                onTarget = UnitManager.Instance.units[i];
                movePos = unit.EllipseCollider.AroundTarget(movePos, onTarget.EllipseCollider, unit.Status.moveSpeed);

                return true;
            }
        }

        movePos = unit.EllipseCollider.TransAreaPos(movePos);

        return false;
    }

    private Vector3 GetMoveVector(Transform _from, Transform _to)
    {
        return unit.Status.moveSpeed * (_from.position - _to.position).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Move : UnitStateBase
{
    public UnitState_Move(Unit _unit, UnitStateBase _base) : base(_unit, _base)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (target == null || unit.notMove)
        {
            unit.StateChange(UnitState.Idle);

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

            Flip(movePos.x, 0);
        }

        unit.Animator.Play("Walk_" + unit.StateNum);
        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.transform.position + movePos, unit.status.moveSpeed.Data);

        unit.StateChange(UnitState.Idle);
    }

    public override void OnExit()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Move : UnitStateBase
{
    private bool onEllipse;

    public UnitState_Move(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (target == null)
        {
            unit.StateChange(UnitState.Idle);

            return;
        }

        if (unit.notMove)
        {
            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) unit.StateChange(UnitState.Attack);

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

            if (movePos == Vector3.zero) unit.StateChange(UnitState.Attack);

            Flip(movePos.x, 0);
        }

        unit.Animator.Play("Walk");

        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.transform.position + movePos, unit.status.moveSpeed.Data);
    }

    public override void OnExit()
    {

    }
}

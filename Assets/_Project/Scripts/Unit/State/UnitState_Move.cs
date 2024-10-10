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
        if (onEllipse)
        {
            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, onTarget.EllipseCollider, EllipseType.Unit, EllipseType.Unit) > 1.1f) onEllipse = false;
            else movePos = unit.EllipseCollider.TransAreaPos(movePos + unit.status.moveSpeed.Data * (unit.transform.position - onTarget.transform.position).normalized);
        }
        else
        {
            onEllipse = OnEllipseEnter();

            if (movePos == Vector3.zero) unit.StateChange(UnitState.Attack);

            if (movePos.x < 0) unit.transform.localScale = new Vector3(1, 1, 1);
            else if (movePos.x > 0) unit.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (unit.notMove) return;

        unit.Animator.Play("Walk");

        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.transform.position + movePos, unit.status.moveSpeed.Data);
    }

    public override void OnExit()
    {

    }
}

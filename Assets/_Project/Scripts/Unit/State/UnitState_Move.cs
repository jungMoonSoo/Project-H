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
        unit.Animator.Play("Walk_" + unit.StateNum);
        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.transform.position + movePos, unit.Status.moveSpeed);

        unit.StateChange(UnitState.Idle);
    }

    public override void OnExit()
    {

    }
}

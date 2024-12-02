using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Move : UnitStateBase
{
    public UnitState_Move(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        unit.Animator.Play("Walk_" + unit.StateNum);
        unit.transform.position = Vector2.MoveTowards(unit.transform.position, unit.transform.position + unit.MovePos, unit.Status.moveSpeed * Time.deltaTime);

        unit.StateChange(UnitState.Idle);
    }

    public override void OnExit()
    {

    }
}

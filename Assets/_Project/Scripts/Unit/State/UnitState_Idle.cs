using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitStateBase
{
    public UnitState_Idle(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (target != null)
        {
            unit.StateChange(UnitState.Move);

            return;
        }

        unit.Animator.Play("Idle");
    }

    public override void OnExit()
    {

    }
}

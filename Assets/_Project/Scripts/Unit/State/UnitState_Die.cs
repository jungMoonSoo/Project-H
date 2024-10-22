using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Die : UnitStateBase
{
    public UnitState_Die(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {
        UnitManager.Instance.units.Remove(unit);

        unit.Animator.Play("Idle");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {

    }
}

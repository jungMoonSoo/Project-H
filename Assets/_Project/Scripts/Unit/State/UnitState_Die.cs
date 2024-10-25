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

        unit.Animator.Play("Idle_" + unit.StateNum);
    }

    public override void OnUpdate()
    {
        unit.gameObject.SetActive(false);

        unit.StateChange(UnitState.Idle);
    }

    public override void OnExit()
    {

    }
}

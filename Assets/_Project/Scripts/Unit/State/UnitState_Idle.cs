using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitStateBase
{
    private float dist;

    public UnitState_Idle(Unit _unit, UnitStateBase _base) : base(_unit, _base)
    {

    }

    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        if (target == null)
        {
            unit.Animator.Play("Idle_" + unit.StateNum);

            return;
        }

        if (!UnitManager.Instance.isPlay) return;


        if (unit.status.mp[0].Data == unit.status.mp[1].Data)
        {
            unit.StateChange(UnitState.Skill);

            return;
        }

        dist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit);

        if (dist <= 1)
        {
            if (unit.notMove) unit.StateChange(UnitState.Attack);
            else if (!OnEllipseEnter()) unit.StateChange(UnitState.Attack);

            return;
        }

        unit.StateChange(UnitState.Move);
    }

    public override void OnExit()
    {

    }
}

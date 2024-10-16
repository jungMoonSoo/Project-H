using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Attack : UnitStateBase
{
    private bool attack;
    private AnimatorStateInfo state;

    public UnitState_Attack(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (OnEllipseEnter()) return;

        if (movePos != Vector3.zero)
        {
            unit.StateChange(UnitState.Idle);

            return;
        }

        unit.Animator.Play("Attack");

        state = unit.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Attack"))
        {
            if (state.normalizedTime % 1 > 0.3f)
            {
                if (!attack)
                {
                    target.status.hp[0].Data -= unit.status.atk.Data;

                    attack = true;
                }
            }
            else attack = false;
        }

        Flip(target.transform.position.x, unit.transform.position.x);
    }

    public override void OnExit()
    {

    }
}

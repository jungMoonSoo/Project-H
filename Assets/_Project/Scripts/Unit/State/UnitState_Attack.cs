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
        if (target == null || unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) > 1)
        {
            unit.StateChange(UnitState.Idle);

            return;
        }

        unit.Animator.Play("Attack_" + unit.StateNum);

        state = unit.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Attack_" + unit.StateNum))
        {
            if (state.normalizedTime % 1 > unit.status.atkAnimPoint.Data)
            {
                if (!attack)
                {
                    if (Attack())
                    {
                        target.status.hp[0].Data -= unit.status.atk.Data;
                        target.status.mp[0].Data += unit.status.mpRegen.Data;
                    }
                    else target.status.mp[0].Data += (int)(unit.status.mpRegen.Data * 0.5f);

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

    private bool Attack()
    {


        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Attack : UnitStateBase
{
    private bool attack;
    private AnimatorStateInfo state;

    public UnitState_Attack(Unit _unit, UnitStateBase _base) : base(_unit, _base)
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

        unit.Animator.Play($"Attack_{unit.StateNum}");

        state = unit.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName($"Attack_{unit.StateNum}"))
        {
            if (state.normalizedTime % 1 > 0.9f) unit.StateChange(UnitState.Idle);
            else if (state.normalizedTime % 1 > unit.Status.atkAnimPoint)
            {
                if (!attack)
                {
                    if (target.OnDamage(false, unit.Status, 0)) unit.Status.mp[0].Data += unit.Status.mpRegen;
                    else unit.Status.mp[0].Data += (int)(unit.Status.mpRegen * 0.5f);

                    attack = true;
                }
            }
        }
    }

    public override void OnExit()
    {

    }
}

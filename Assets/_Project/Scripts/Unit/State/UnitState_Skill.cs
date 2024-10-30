using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Skill : UnitStateBase
{
    private bool attack;
    private AnimatorStateInfo state;

    public UnitState_Skill(Unit _unit, UnitStateBase _base) : base(_unit, _base)
    {

    }

    public override void OnEnter()
    {
        unit.status.mp[0].Data = 0;
    }

    public override void OnUpdate()
    {
        if (unit.skills.Length < unit.StateNum + 1) return;

        unit.Animator.Play("Skill_" + unit.StateNum);

        state = unit.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Skill_" + unit.StateNum))
        {
            if (state.normalizedTime >= 1f) unit.StateChange(UnitState.Idle);
            else if (state.normalizedTime > unit.skills[unit.StateNum].skillAttackPoint)
            {
                if (!attack)
                {
                    unit.skills[unit.StateNum].OnUseSkill();

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

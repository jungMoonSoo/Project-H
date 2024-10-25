using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Skill : UnitStateBase
{
    private bool attack;
    private AnimatorStateInfo state;

    public UnitState_Skill(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (unit.skills.Length < unit.SkillNum + 1) return;

        unit.Animator.Play("Skill_" + unit.SkillNum);

        state = unit.Animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Skill_" + unit.SkillNum))
        {
            if (state.normalizedTime > unit.skills[unit.SkillNum].skillAttackPoint)
            {
                if (!attack)
                {
                    unit.skills[unit.SkillNum].OnUseSkill();

                    if (Attack()) target.status.hp[0].Data -= unit.status.atk.Data;

                    attack = true;
                }
            }
            else attack = false;
        }

        if (state.normalizedTime >= 1f) unit.StateChange(UnitState.Idle);

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

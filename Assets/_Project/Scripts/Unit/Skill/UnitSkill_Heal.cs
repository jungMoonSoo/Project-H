using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill_Heal : UnitSkillBase
{
    public override void OnUseSkill()
    {
        OnEllipseEnter(transform.position);

        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].status.hp[0].Data += 100;
        }
    }
}

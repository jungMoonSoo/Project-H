using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill_Heal : UnitSkillBase
{
    public override void Execute()
    {
        OnEllipseEnter(transform.position);

        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].Status.hp[0].Data += 100;
        }
    }

    private void OnEllipseEnter(Vector2 _pos)
    {
        targets.Clear();

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != unit && unit.EllipseCollider.OnEllipseEnter(_pos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Skill, EllipseType.Unit) <= 1)
            {
                targets.Add(UnitManager.Instance.units[i]);
            }
        }
    }
}

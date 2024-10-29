using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill_Heal_Invoke : MonoBehaviour, ISkillInvoke<Unit>
{
    public float skillAttackPoint;

    protected readonly List<Unit> targets = new();

    public void Action(Unit _unit)
    {
        OnEllipseEnter(_unit, transform.position);

        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].status.hp[0].Data += 100;
        }
    }

    private void OnEllipseEnter(Unit _unit, Vector2 _pos)
    {
        targets.Clear();

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != _unit && _unit.EllipseCollider.OnEllipseEnter(_pos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Skill, EllipseType.Unit) <= 1)
            {
                targets.Add(UnitManager.Instance.units[i]);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSkillBase : MonoBehaviour
{
    protected Unit unit;

    public float skillAttackPoint;

    protected readonly List<Unit> targets = new();

    public void Awake()
    {
        TryGetComponent(out unit);
    }

    public abstract void OnUseSkill();

    protected void OnEllipseEnter(Vector2 _pos)
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

using System;
using System.Collections.Generic;
using UnityEngine;

public class TestSkillEffect : SkillObjectBase
{
    private readonly List<Unit> unitList = new(128);
    
    private EllipseCollider ellipseCollider = null;

    public override void ApplyEffect()
    {
        Unit[] units = Targets;
        foreach (Unit unit in units)
        {
            unit.OnDamage((int)Influence);
        }

        Destroy(gameObject);
    }

    public override void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public override Unit[] GetTargets()
    {
        SetCollider();
        unitList.Clear();
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            if(ellipseCollider.OnEllipseEnter(transform.position, unit.EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1f)
            {
                unitList.Add(unit);
            }
        }

        return unitList.ToArray();
    }

    private void SetCollider()
    {
        if (ellipseCollider == null)
        {
            ellipseCollider = GetComponent<EllipseCollider>();
            if (ellipseCollider != null)
            {
                ellipseCollider.ranges = new List<Vector2>() { EffectRange * 0.5f };
                ellipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);
            }
        }
    }
}

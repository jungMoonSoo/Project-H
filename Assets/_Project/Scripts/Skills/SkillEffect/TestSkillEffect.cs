using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSkillEffect : SkillObjectBase
{
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
        return UnitManager.Instance.units.Where(x => ellipseCollider.OnEllipseEnter(transform.position, x.EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1f).ToArray();
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

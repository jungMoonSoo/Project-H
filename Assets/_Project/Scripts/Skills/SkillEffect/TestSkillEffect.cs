using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSkillEffect : SkillObjectBase
{
    private NewEllipseCollider ellipseCollider = null;

    public override void ApplyEffect()
    {
        Unidad[] units = Targets;
        foreach (Unidad unit in units)
        {
            // unit.OnDamage((int)Influence);
        }

        Destroy(gameObject);
    }

    public override void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public override Unidad[] GetTargets()
    {
        SetCollider();
        return UnitManager.Instance.units.Where(x => ellipseCollider.OnEllipseEnter(x.skillCollider)).ToArray();
    }

    private void SetCollider()
    {
        if (ellipseCollider == null)
        {
            ellipseCollider = GetComponent<NewEllipseCollider>();
        }
    }
}

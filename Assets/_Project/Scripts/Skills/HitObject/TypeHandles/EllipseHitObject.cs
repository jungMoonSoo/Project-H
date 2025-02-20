using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseHitObject : HitObjectBase
{
    [SerializeField] private EllipseCollider effectCollider;

    public override Vector2 GetAreaSize() => effectCollider.size;

    protected override Unidad[] Targeting() => RangeTargeting.GetTargets(Caster.Owner, TargetType, CreatePos, TargetPos, effectCollider.Radius);
}

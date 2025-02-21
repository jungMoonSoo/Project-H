using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseHitObject : HitObjectBase
{
    [SerializeField] private EllipseCollider coll;

    public override Vector2 GetAreaSize() => coll.size;

    protected override Unidad[] Targeting() => RangeTargeting.GetTargets(Caster.Owner, TargetType, CreatePos, TargetPos, coll.Radius);
}

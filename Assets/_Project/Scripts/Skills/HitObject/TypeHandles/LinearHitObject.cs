using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearHitObject : HitObjectBase
{
    [SerializeField] private LinearCollider coll;
    [SerializeField] private int splitCount = 1;

    private Vector2 SplitSize => new(coll.size.x, coll.size.y / splitCount);

    public override Vector2 GetAreaSize() => new(coll.size.y, coll.size.x);

    protected override Unidad[] Targeting()
    {
        coll.direction = TargetPos.normalized;

        return RangeTargeting.GetTargets(Caster.Owner, TargetType, transform.position, TargetPos, SplitSize);
    }
}

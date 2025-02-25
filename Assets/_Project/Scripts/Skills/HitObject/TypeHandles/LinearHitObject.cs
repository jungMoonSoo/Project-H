using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearHitObject : HitObjectBase
{
    [SerializeField] private LinearCollider coll;

    public override Vector2 GetAreaSize() => new(coll.size.y, coll.size.x);

    protected override Unidad[] Targeting()
    {
        coll.Direction = TargetPos - transform.position;

        return TargetingFilter.GetFilteredTargets(GetTargets(Caster.Owner, TargetType, coll), TargetPos);
    }
}

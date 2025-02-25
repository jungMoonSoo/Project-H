using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcHitObject : HitObjectBase
{
    [SerializeField] private ArcCollider coll;

    public override Vector2 GetAreaSize() => coll.Radius;

    protected override Unidad[] Targeting()
    {
        coll.Direction = TargetPos - transform.position;

        return TargetingFilter.GetFilteredTargets(GetTargets(Caster.Owner, TargetType, coll), TargetPos);
    }
}

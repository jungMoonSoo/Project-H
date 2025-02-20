using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearHitObject : HitObjectBase
{
    [SerializeField] private Vector2 size;
    [SerializeField] private int splitCount = 1;

    private Vector2 SplitSize => new(size.x, size.y / splitCount);

    public override Vector2 GetAreaSize() => new(size.y, size.x);

    protected override Unidad[] Targeting() => RangeTargeting.GetTargets(Caster.Owner, TargetType, transform.position, TargetPos, SplitSize);
}

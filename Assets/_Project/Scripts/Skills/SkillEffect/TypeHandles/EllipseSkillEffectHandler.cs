using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private EllipseCollider effectCollider;

    public override Vector2 GetAreaSize() => effectCollider.size;

    protected override Unidad[] Targeting() => TargetingSystem.GetTargets(Owner, TargetType, CastingPosition, transform.position + effectCollider.Center, effectCollider.Radius);
}

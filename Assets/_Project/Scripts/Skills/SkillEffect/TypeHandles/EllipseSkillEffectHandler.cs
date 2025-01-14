using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private EllipseCollider effectCollider;

    public override Vector2 GetAreaSize() => effectCollider.Radius;

    protected override Unidad[] Targeting(TargetType targetType) => TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, (Vector2)transform.position + effectCollider.center, effectCollider.Radius);
}

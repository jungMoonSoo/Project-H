using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private EllipseCollider effectCollider;

    public override void Init(Unidad caster, Vector2 position)
    {
        effectCollider.size = caster.Status.skillInfo.skillArea.areaSize;

        base.Init(caster, position);
    }

    protected override Unidad[] Targeting(TargetType targetType) => TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, (Vector2)transform.position + effectCollider.center, effectCollider.size * 0.5f);
}

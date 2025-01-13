using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private EllipseCollider effectCollider;

    public override void Init(Unidad caster, Vector2 position)
    {
        base.Init(caster, position);

        effectCollider.size = Caster.Status.skillInfo.skillArea.areaSize;
    }

    protected override Unidad[] Targeting(TargetType targetType) => TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, (Vector2)transform.position + effectCollider.center, effectCollider.size * 0.5f);
}

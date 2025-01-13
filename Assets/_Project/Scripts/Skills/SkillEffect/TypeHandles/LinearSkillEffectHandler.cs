using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSkillEffectHandler : SkillEffectHandlerBase
{
    private Vector2 size;

    public override void Init(Unidad caster, Vector2 position)
    {
        base.Init(caster, position);

        size = Caster.Status.skillInfo.skillArea.areaSize;

        (size.x, size.y) = (size.y, size.x);
    }

    protected override Unidad[] Targeting(TargetType targetType) => TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, transform.position, size);
}

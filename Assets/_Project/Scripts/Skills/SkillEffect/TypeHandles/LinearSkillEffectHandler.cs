using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private Vector2 size;

    public override Vector2 GetAreaSize() => size;

    public override void Init(Unidad caster, Vector2 position)
    {
        (size.x, size.y) = (size.y, size.x);

        base.Init(caster, position);
    }

    protected override Unidad[] Targeting(TargetType targetType) => TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, transform.position, size);
}

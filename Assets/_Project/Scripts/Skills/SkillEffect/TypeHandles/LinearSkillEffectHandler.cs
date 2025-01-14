using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSkillEffectHandler : SkillEffectHandlerBase
{
    [SerializeField] private Vector2 size;

    public override Vector2 GetAreaSize() => new(size.y, size.x);

    protected override Unidad[] Targeting() => TargetingSystem.GetTargets(Caster.Owner, TargetType, Caster.transform.position, transform.position, size);
}

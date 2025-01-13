using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerSkillHandles : MonoBehaviour, ISkillEffectTriggerEvent, ISkillEffectPositioner, ISkillEffectFinishEvent
{
    [SerializeField] private float skillCoefficient = 200f;

    public void OnTrigger(SkillEffectHandlerBase handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.Caster.NowAttackStatus, handler.Caster.NowDefenceStatus, skillCoefficient, 0, ElementType.None);

        foreach (Unidad unit in handler.Targets) unit.OnDamage((int)callback.value, callback.type);
    }

    public void SetPosition(SkillEffectHandlerBase handler, Vector2 position)
    {
        handler.transform.position = position;
    }

    public void OnFinish(SkillEffectHandlerBase handler)
    {
        Destroy(handler.gameObject);
    }
}

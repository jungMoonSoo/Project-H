using UnityEngine;

public class DamageTriggerEvent: MonoBehaviour, ISkillEffectTriggerEvent
{
    public void OnTrigger(SkillEffectHandlerBase handler)
    {
        foreach (Unidad unit in handler.Targets)
        {
            unit.OnDamage(10, DamageType.Normal);
        }
    }
}
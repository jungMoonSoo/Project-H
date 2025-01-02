using UnityEngine;

public class DamageTriggerEvent: MonoBehaviour, ISkillEffectTriggerEvent
{
    public void OnTrigger(SkillEffectHandler handler)
    {
        foreach (Unidad unit in handler.Targets)
        {
            unit.OnDamage(10, DamageType.Normal);
        }
    }
}
using UnityEngine;

public class HealTriggerEvent: MonoBehaviour, ISkillEffectTriggerEvent
{
    [SerializeField] private float skillCoefficient = 200f;

    public void OnTrigger(SkillEffectHandlerBase handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.AttackStatus, handler.DefenceStatus, skillCoefficient, 0, ElementType.None);

        foreach (Unidad unit in handler.Targets) unit.OnHeal((int)callback.value, callback.type);
    }
}
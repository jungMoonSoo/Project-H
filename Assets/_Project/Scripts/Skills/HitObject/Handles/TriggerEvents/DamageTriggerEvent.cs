using UnityEngine;

public class DamageTriggerEvent: MonoBehaviour, IHitObjectTriggerEvent
{
    [SerializeField] private float skillCoefficient = 200f;
    [SerializeField] private int hitCount = 1;

    public void OnTrigger(HitObjectBase handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.Caster.NowAttackStatus, handler.Caster.NowDefenceStatus, skillCoefficient, 0, ElementType.None);

        Unidad[] targets = handler.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (i == hitCount) break;

            targets[i].OnDamage((int)callback.value, callback.type, handler.EffectManager?.GetEffect(targets[i].transform));
        }
    }
}
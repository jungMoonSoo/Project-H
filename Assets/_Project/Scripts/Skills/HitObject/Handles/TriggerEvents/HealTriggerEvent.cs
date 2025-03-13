using UnityEngine;

public class HealTriggerEvent: MonoBehaviour, IHitObjectTriggerEvent
{
    [SerializeField] private float skillCoefficient = 200f;
    [SerializeField] private int maxHitTarget = 1;

    private AttackStatus attackStatus;
    private DefenceStatus defenceStatus;

    public void Init(Unidad caster)
    {
        attackStatus = caster.NowAttackStatus;
        defenceStatus = caster.NowDefenceStatus;
    }

    public void OnTrigger(HitObject handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(attackStatus, defenceStatus, skillCoefficient, 0, ElementType.None);

        Unidad[] targets = handler.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (i == maxHitTarget) break;

            targets[i].OnHeal((int)callback.value, callback.type);
        }
    }
}
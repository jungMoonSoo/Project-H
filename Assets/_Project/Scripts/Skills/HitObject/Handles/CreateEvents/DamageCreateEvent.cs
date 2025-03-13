using UnityEngine;

public class DamageCreateEvent : MonoBehaviour, IHitObjectCreateEvent
{
    [SerializeField] private float coefficient = 200f;
    [SerializeField] private int maxHitTarget = 1;

    private AttackStatus attackStatus;
    private DefenceStatus defenceStatus;

    public void Init(Unidad caster)
    {
        attackStatus = caster.NowAttackStatus;
        defenceStatus = caster.NowDefenceStatus;
    }

    public void OnCreate(HitObject handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(attackStatus, defenceStatus, coefficient, 0, ElementType.None);

        Unidad[] targets = handler.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (i == maxHitTarget) break;

            targets[i].OnDamage((int)callback.value, callback.type, handler.GetEffect(targets[i].transform));
        }
    }
}

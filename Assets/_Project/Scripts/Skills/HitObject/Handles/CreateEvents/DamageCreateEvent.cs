using UnityEngine;

public class DamageCreateEvent : MonoBehaviour, IHitObjectCreateEvent
{
    [SerializeField] private float coefficient = 200f;
    [SerializeField] private int maxHitTarget = 1;

    public void OnCreate(HitObjectBase handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.Caster.NowAttackStatus, handler.Caster.NowDefenceStatus, coefficient, 0, ElementType.None);

        Unidad[] targets = handler.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (i == maxHitTarget) break;

            targets[i].OnDamage((int)callback.value, callback.type, handler.GetEffect(targets[i].transform));
        }
    }
}

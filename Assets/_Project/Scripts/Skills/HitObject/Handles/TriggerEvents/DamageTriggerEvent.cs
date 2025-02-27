using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerEvent: MonoBehaviour, IHitObjectTriggerEvent
{
    [SerializeField] private float skillCoefficient = 200f;
    [SerializeField] private int maxHitTarget = 1;
    [SerializeField] private int maxHitsOnTarget = 1;

    private int nowHitTarget;

    private readonly Dictionary<Unidad, int> hitsOnTargets = new();

    public void Init(HitObject handler)
    {
        nowHitTarget = 0;
        hitsOnTargets.Clear();
    }

    public void OnTrigger(HitObject handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.Caster.NowAttackStatus, handler.Caster.NowDefenceStatus, skillCoefficient, 0, ElementType.None);

        Unidad[] targets = handler.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (nowHitTarget == maxHitTarget) break;

            if (hitsOnTargets.ContainsKey(targets[i]))
            {
                if (hitsOnTargets[targets[i]] == maxHitsOnTarget) continue;
            }
            else hitsOnTargets.Add(targets[i], 0);

            targets[i].OnDamage((int)callback.value, callback.type, handler.GetEffect(targets[i].transform));

            hitsOnTargets[targets[i]]++;
            nowHitTarget++;
        }
    }
}
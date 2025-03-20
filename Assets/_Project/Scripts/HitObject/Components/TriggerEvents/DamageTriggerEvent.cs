using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerEvent: MonoBehaviour, IHitObjectTriggerEvent
{
    [SerializeField] private float skillCoefficient = 200f;
    [SerializeField] private int maxHitTarget = 1;
    [SerializeField] private int maxHitsOnTarget = 1;

    private AttackStatus attackStatus;
    private DefenceStatus defenceStatus;

    /// <summary>
    /// 공격을 진행 한 횟수 기록 maxHitTarget 이상 공격 불가
    /// </summary>
    private int nowHitTarget;

    /// <summary>
    /// 해당 유닛을 공격한 횟수 기록 maxHitsOnTarget 이상 공격 불가
    /// </summary>
    private readonly Dictionary<Unidad, int> hitsOnTargets = new();

    public void Init(Unidad caster)
    {
        nowHitTarget = 0;
        hitsOnTargets.Clear();

        attackStatus = caster.NowAttackStatus;
        defenceStatus = caster.NowDefenceStatus;
    }

    public void OnEvent(HitObject hitObject)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(attackStatus, defenceStatus, skillCoefficient, 0, ElementType.None);

        Unidad[] targets = hitObject.Targets;

        for (int i = 0; i < targets.Length; i++)
        {
            if (nowHitTarget == maxHitTarget) break;

            if (hitsOnTargets.ContainsKey(targets[i]))
            {
                if (hitsOnTargets[targets[i]] == maxHitsOnTarget) continue;
            }
            else hitsOnTargets.Add(targets[i], 0);

            targets[i].OnDamage((int)callback.value, callback.type, hitObject.GetEffect(targets[i].transform));

            hitsOnTargets[targets[i]]++;
            nowHitTarget++;
        }
    }
}
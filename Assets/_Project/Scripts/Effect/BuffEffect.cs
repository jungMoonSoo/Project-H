using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Buff", fileName = "NewBuffEffect")]
public class BuffEffect : ScriptableObject, IStatusEffect
{
    [SerializeField] private int id;

    [SerializeField] private int count;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public int Id => id;

    public int Count => count;

    public StatusManager Status { get; private set; }

    public void Apply(StatusManager _status)
    {
        Status = _status;

        Status.StatusEffects.Add(this);

        SetStatus(true);
    }

    public void Check(int _count)
    {
        if (_count >= Count) Remove();
    }

    public void Remove()
    {
        Status.StatusEffects.Remove(this);

        SetStatus(false);
    }

    private void SetStatus(bool _apply)
    {
        SetAttackStatus(_apply ? 1 : -1);
        SetDefenceStatus(_apply ? 1 : -1);
    }

    private void SetAttackStatus(int _value)
    {
        Status.AttackStatusEffect.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;
        Status.AttackStatusEffect.physicalDamage += attackStatus.physicalDamage * _value;
        Status.AttackStatusEffect.magicDamage += attackStatus.magicDamage * _value;

        Status.AttackStatusEffect.physicalCriticalDamage += attackStatus.physicalCriticalDamage * _value;
        Status.AttackStatusEffect.magicCriticalDamage += attackStatus.magicCriticalDamage * _value;

        Status.AttackStatusEffect.physicalCriticalProbability += attackStatus.physicalCriticalProbability * _value;
        Status.AttackStatusEffect.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;

        Status.AttackStatusEffect.accuracy += attackStatus.accuracy * _value;

        Status.AttackStatusEffect.fireDamageBonus += attackStatus.fireDamageBonus * _value;
        Status.AttackStatusEffect.waterDamageBonus += attackStatus.waterDamageBonus * _value;
        Status.AttackStatusEffect.airDamageBonus += attackStatus.airDamageBonus * _value;
        Status.AttackStatusEffect.earthDamageBonus += attackStatus.earthDamageBonus * _value;
        Status.AttackStatusEffect.lightDamageBonus += attackStatus.lightDamageBonus * _value;
        Status.AttackStatusEffect.darkDamageBonus += attackStatus.darkDamageBonus * _value;
    }

    private void SetDefenceStatus(int _value)
    {
        Status.DefenceStatusEffect.physicalDefence += defenceStatus.physicalDefence * _value;
        Status.DefenceStatusEffect.magicDefence += defenceStatus.magicDefence * _value;

        Status.DefenceStatusEffect.physicalCriticalResistance += defenceStatus.physicalCriticalResistance * _value;
        Status.DefenceStatusEffect.magicCriticalResistance += defenceStatus.magicCriticalResistance * _value;

        Status.DefenceStatusEffect.dodgeProbability += defenceStatus.dodgeProbability * _value;

        Status.DefenceStatusEffect.fireResistanceBonus += defenceStatus.fireResistanceBonus * _value;
        Status.DefenceStatusEffect.waterResistanceBonus += defenceStatus.waterResistanceBonus * _value;
        Status.DefenceStatusEffect.airResistanceBonus += defenceStatus.airResistanceBonus * _value;
        Status.DefenceStatusEffect.earthResistanceBonus += defenceStatus.earthResistanceBonus * _value;

        Status.DefenceStatusEffect.lightResistanceBonus += defenceStatus.lightResistanceBonus * _value;
        Status.DefenceStatusEffect.darkResistanceBonus += defenceStatus.darkResistanceBonus * _value;
    }
}

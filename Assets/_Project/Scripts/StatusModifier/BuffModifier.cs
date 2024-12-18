using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatusModifier/Buff", fileName = "NewBuff")]
public class BuffModifier : ScriptableObject, IStatusModifier
{
    [SerializeField] private int id;

    [SerializeField] private int count;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public int Id => id;

    public int Count => count;

    public StatusManager Status { get; private set; }

    public void Apply(StatusManager _status, int _time)
    {
        Status = _status;

        Status.StatusModifiers.Add(this, _time);

        SetStatus(true);
    }

    public void Check(int _count)
    {
        if (_count >= Count) Remove();
    }

    public void Remove()
    {
        Status.StatusModifiers.Remove(this);

        SetStatus(false);
    }

    private void SetStatus(bool _apply)
    {
        SetAttackStatus(_apply ? 1 : -1);
        SetDefenceStatus(_apply ? 1 : -1);
    }

    private void SetAttackStatus(int _value)
    {
        Status.AttackStatusModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;
        Status.AttackStatusModifier.physicalDamage += attackStatus.physicalDamage * _value;
        Status.AttackStatusModifier.magicDamage += attackStatus.magicDamage * _value;

        Status.AttackStatusModifier.physicalCriticalDamage += attackStatus.physicalCriticalDamage * _value;
        Status.AttackStatusModifier.magicCriticalDamage += attackStatus.magicCriticalDamage * _value;

        Status.AttackStatusModifier.physicalCriticalProbability += attackStatus.physicalCriticalProbability * _value;
        Status.AttackStatusModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;

        Status.AttackStatusModifier.accuracy += attackStatus.accuracy * _value;

        Status.AttackStatusModifier.fireDamageBonus += attackStatus.fireDamageBonus * _value;
        Status.AttackStatusModifier.waterDamageBonus += attackStatus.waterDamageBonus * _value;
        Status.AttackStatusModifier.airDamageBonus += attackStatus.airDamageBonus * _value;
        Status.AttackStatusModifier.earthDamageBonus += attackStatus.earthDamageBonus * _value;
        Status.AttackStatusModifier.lightDamageBonus += attackStatus.lightDamageBonus * _value;
        Status.AttackStatusModifier.darkDamageBonus += attackStatus.darkDamageBonus * _value;
    }

    private void SetDefenceStatus(int _value)
    {
        Status.DefenceStatusModifier.physicalDefence += defenceStatus.physicalDefence * _value;
        Status.DefenceStatusModifier.magicDefence += defenceStatus.magicDefence * _value;

        Status.DefenceStatusModifier.physicalCriticalResistance += defenceStatus.physicalCriticalResistance * _value;
        Status.DefenceStatusModifier.magicCriticalResistance += defenceStatus.magicCriticalResistance * _value;

        Status.DefenceStatusModifier.dodgeProbability += defenceStatus.dodgeProbability * _value;

        Status.DefenceStatusModifier.fireResistanceBonus += defenceStatus.fireResistanceBonus * _value;
        Status.DefenceStatusModifier.waterResistanceBonus += defenceStatus.waterResistanceBonus * _value;
        Status.DefenceStatusModifier.airResistanceBonus += defenceStatus.airResistanceBonus * _value;
        Status.DefenceStatusModifier.earthResistanceBonus += defenceStatus.earthResistanceBonus * _value;

        Status.DefenceStatusModifier.lightResistanceBonus += defenceStatus.lightResistanceBonus * _value;
        Status.DefenceStatusModifier.darkResistanceBonus += defenceStatus.darkResistanceBonus * _value;
    }
}

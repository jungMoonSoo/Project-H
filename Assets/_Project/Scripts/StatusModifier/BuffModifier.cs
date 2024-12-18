using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/Buff", fileName = "NewBuff")]
public class BuffModifier : ScriptableObject, IUnitModifier
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

        Status.UnitModifiers.Add(this, _time);

        SetStatus(true);
    }

    public void Check(int _count)
    {
        if (_count >= Count) Remove();
    }

    public void Remove()
    {
        Status.UnitModifiers.Remove(this);

        SetStatus(false);
    }

    private void SetStatus(bool _apply)
    {
        SetAttackStatus(_apply ? 1 : -1);
        SetDefenceStatus(_apply ? 1 : -1);
    }

    private void SetAttackStatus(int _value)
    {
        Status.AttackUnitModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;
        Status.AttackUnitModifier.physicalDamage += attackStatus.physicalDamage * _value;
        Status.AttackUnitModifier.magicDamage += attackStatus.magicDamage * _value;

        Status.AttackUnitModifier.physicalCriticalDamage += attackStatus.physicalCriticalDamage * _value;
        Status.AttackUnitModifier.magicCriticalDamage += attackStatus.magicCriticalDamage * _value;

        Status.AttackUnitModifier.physicalCriticalProbability += attackStatus.physicalCriticalProbability * _value;
        Status.AttackUnitModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * _value;

        Status.AttackUnitModifier.accuracy += attackStatus.accuracy * _value;

        Status.AttackUnitModifier.fireDamageBonus += attackStatus.fireDamageBonus * _value;
        Status.AttackUnitModifier.waterDamageBonus += attackStatus.waterDamageBonus * _value;
        Status.AttackUnitModifier.airDamageBonus += attackStatus.airDamageBonus * _value;
        Status.AttackUnitModifier.earthDamageBonus += attackStatus.earthDamageBonus * _value;
        Status.AttackUnitModifier.lightDamageBonus += attackStatus.lightDamageBonus * _value;
        Status.AttackUnitModifier.darkDamageBonus += attackStatus.darkDamageBonus * _value;
    }

    private void SetDefenceStatus(int _value)
    {
        Status.DefenceUnitModifier.physicalDefence += defenceStatus.physicalDefence * _value;
        Status.DefenceUnitModifier.magicDefence += defenceStatus.magicDefence * _value;

        Status.DefenceUnitModifier.physicalCriticalResistance += defenceStatus.physicalCriticalResistance * _value;
        Status.DefenceUnitModifier.magicCriticalResistance += defenceStatus.magicCriticalResistance * _value;

        Status.DefenceUnitModifier.dodgeProbability += defenceStatus.dodgeProbability * _value;

        Status.DefenceUnitModifier.fireResistanceBonus += defenceStatus.fireResistanceBonus * _value;
        Status.DefenceUnitModifier.waterResistanceBonus += defenceStatus.waterResistanceBonus * _value;
        Status.DefenceUnitModifier.airResistanceBonus += defenceStatus.airResistanceBonus * _value;
        Status.DefenceUnitModifier.earthResistanceBonus += defenceStatus.earthResistanceBonus * _value;

        Status.DefenceUnitModifier.lightResistanceBonus += defenceStatus.lightResistanceBonus * _value;
        Status.DefenceUnitModifier.darkResistanceBonus += defenceStatus.darkResistanceBonus * _value;
    }
}

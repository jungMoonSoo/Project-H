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

    public void Apply(StatusManager status, int time)
    {
        Status = status;

        Status.UnitModifiers.Add(this, time);

        SetStatus(true);
    }

    public void Check(int count)
    {
        if (count >= Count) Remove();
    }

    public void Remove()
    {
        Status.UnitModifiers.Remove(this);

        SetStatus(false);
    }

    private void SetStatus(bool apply)
    {
        SetAttackStatus(apply ? 1 : -1);
        SetDefenceStatus(apply ? 1 : -1);
    }

    private void SetAttackStatus(int value)
    {
        Status.AttackUnitModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * value;
        Status.AttackUnitModifier.physicalDamage += attackStatus.physicalDamage * value;
        Status.AttackUnitModifier.magicDamage += attackStatus.magicDamage * value;

        Status.AttackUnitModifier.physicalCriticalDamage += attackStatus.physicalCriticalDamage * value;
        Status.AttackUnitModifier.magicCriticalDamage += attackStatus.magicCriticalDamage * value;

        Status.AttackUnitModifier.physicalCriticalProbability += attackStatus.physicalCriticalProbability * value;
        Status.AttackUnitModifier.magicCriticalProbability += attackStatus.magicCriticalProbability * value;

        Status.AttackUnitModifier.accuracy += attackStatus.accuracy * value;

        Status.AttackUnitModifier.fireDamageBonus += attackStatus.fireDamageBonus * value;
        Status.AttackUnitModifier.waterDamageBonus += attackStatus.waterDamageBonus * value;
        Status.AttackUnitModifier.airDamageBonus += attackStatus.airDamageBonus * value;
        Status.AttackUnitModifier.earthDamageBonus += attackStatus.earthDamageBonus * value;
        Status.AttackUnitModifier.lightDamageBonus += attackStatus.lightDamageBonus * value;
        Status.AttackUnitModifier.darkDamageBonus += attackStatus.darkDamageBonus * value;
    }

    private void SetDefenceStatus(int value)
    {
        Status.DefenceUnitModifier.physicalDefence += defenceStatus.physicalDefence * value;
        Status.DefenceUnitModifier.magicDefence += defenceStatus.magicDefence * value;

        Status.DefenceUnitModifier.physicalCriticalResistance += defenceStatus.physicalCriticalResistance * value;
        Status.DefenceUnitModifier.magicCriticalResistance += defenceStatus.magicCriticalResistance * value;

        Status.DefenceUnitModifier.dodgeProbability += defenceStatus.dodgeProbability * value;

        Status.DefenceUnitModifier.fireResistanceBonus += defenceStatus.fireResistanceBonus * value;
        Status.DefenceUnitModifier.waterResistanceBonus += defenceStatus.waterResistanceBonus * value;
        Status.DefenceUnitModifier.airResistanceBonus += defenceStatus.airResistanceBonus * value;
        Status.DefenceUnitModifier.earthResistanceBonus += defenceStatus.earthResistanceBonus * value;

        Status.DefenceUnitModifier.lightResistanceBonus += defenceStatus.lightResistanceBonus * value;
        Status.DefenceUnitModifier.darkResistanceBonus += defenceStatus.darkResistanceBonus * value;
    }
}

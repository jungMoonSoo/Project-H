using System.Collections.Generic;
using UnityEngine;

public class ModifierManager
{
    private readonly Unidad unidad;

    private readonly NormalStatus nowNormalStatus = new();
    private readonly AttackStatus nowAttackStatus = new();
    private readonly DefenceStatus nowDefenceStatus = new();

    public NormalStatus NowNormalStatus => nowNormalStatus;
    public AttackStatus NowAttackStatus => nowAttackStatus;
    public DefenceStatus NowDefenceStatus => nowDefenceStatus;

    private readonly List<ModifierBase> removeModifiers = new();
    private readonly Dictionary<ModifierBase, ModifierInfo> applyModifiers = new();

    public ModifierManager(Unidad unidad)
    {
        this.unidad = unidad;

        Clear();
    }

    public void SetUnitDefaltStatus(int level)
    {
        if (level < 0) level = 0;

        defaultNormalStatus = unidad.Status.normalStatus[unidad.Status.normalStatus.Length > level ? level : unidad.Status.normalStatus.Length - 1];
        defaultAttackStatus = unidad.Status.attackStatus[unidad.Status.attackStatus.Length > level ? level : unidad.Status.attackStatus.Length - 1];
        defaultDefenceStatus = unidad.Status.defenceStatus[unidad.Status.defenceStatus.Length > level ? level : unidad.Status.defenceStatus.Length - 1];

        SetNowNormalStatus();
        SetNowAttackStatus();
        SetNowDefenceStatus();
    }

    public void Add(ModifierBase modifier)
    {
        ModifierInfo modifierInfo = new(Time.time, modifier.CycleCount);

        if (applyModifiers.ContainsKey(modifier)) applyModifiers[modifier] = modifierInfo;
        else applyModifiers.Add(modifier, modifierInfo);

        modifier.Add(unidad);
    }

    public void Remove(ModifierBase modifier)
    {
        if (applyModifiers.ContainsKey(modifier)) applyModifiers.Remove(modifier);

        modifier.Remove(unidad);
        removeModifiers.Remove(modifier);
    }

    public void Clear()
    {
        foreach (ModifierBase modifier in applyModifiers.Keys) Remove(modifier);

        removeModifiers.Clear();

        SetUnitDefaltStatus(0);

        InitNormalStatus(normalModifier, 0);
        InitNormalStatus(normalModifierMultiply, 1);

        InitAttackStatus(attackModifier, 0);
        InitAttackStatus(attackModifierMultiply, 1);

        InitDefenceStatus(defenceModifier, 0);
        InitDefenceStatus(defenceModifierMultiply, 1);
    }

    public void CheckCycle()
    {
        foreach (ModifierBase modifier in applyModifiers.Keys)
        {
            if (applyModifiers[modifier].applyTime > Time.time) continue;

            applyModifiers[modifier].applyTime = Time.time + modifier.IntervalTime;
            applyModifiers[modifier].cycleCount -= modifier.Cycle(unidad);

            if (applyModifiers[modifier].cycleCount <= 0) removeModifiers.Add(modifier);
        }

        if (removeModifiers.Count == 0) return;

        for (int i = removeModifiers.Count - 1; i >= 0; i--) Remove(removeModifiers[i]);
    }

    #region ◇◇ 스테이터스 적용 ◇◇
    public void SetModifier(NormalStatus modifierStatus, bool apply) => SetNormalStatus(normalModifier, modifierStatus, apply ? 1 : -1);
    public void SetModifier(AttackStatus modifierStatus, bool apply) => SetAttackStatus(attackModifier, modifierStatus, apply ? 1 : -1);
    public void SetModifier(DefenceStatus modifierStatus, bool apply) => SetDefenceStatus(defenceModifier, modifierStatus, apply ? 1 : -1);

    public void SetModifierMultiply(NormalStatus modifierStatus, bool apply) => SetNormalStatus(normalModifierMultiply, modifierStatus, apply ? 1 : -1);
    public void SetModifierMultiply(AttackStatus modifierStatus, bool apply) => SetAttackStatus(attackModifierMultiply, modifierStatus, apply ? 1 : -1);
    public void SetModifierMultiply(DefenceStatus modifierStatus, bool apply) => SetDefenceStatus(defenceModifierMultiply, modifierStatus, apply ? 1 : -1);

    private void InitNormalStatus(NormalStatus status, int value)
    {
        status.maxHp = value;
        status.maxMp = value;

        status.attackSpeed = value;
        status.moveSpeed = value;
    }

    private void InitAttackStatus(AttackStatus status, int value)
    {
        status.magicCriticalProbability = value;
        status.physicalDamage = value;
        status.magicDamage = value;

        status.physicalCriticalDamage = value;
        status.magicCriticalDamage = value;

        status.physicalCriticalProbability = value;
        status.magicCriticalProbability = value;

        status.accuracy = value;

        status.fireDamageBonus = value;
        status.waterDamageBonus = value;
        status.airDamageBonus = value;
        status.earthDamageBonus = value;
        status.lightDamageBonus = value;
        status.darkDamageBonus = value;
    }

    private void InitDefenceStatus(DefenceStatus status, int value)
    {
        status.physicalDefence = value;
        status.magicDefence = value;

        status.physicalCriticalResistance = value;
        status.magicCriticalResistance = value;

        status.dodgeProbability = value;

        status.fireResistanceBonus = value;
        status.waterResistanceBonus = value;
        status.airResistanceBonus = value;
        status.earthResistanceBonus = value;

        status.lightResistanceBonus = value;
        status.darkResistanceBonus = value;
    }

    private void SetNormalStatus(NormalStatus status, NormalStatus modifierStatus, int value)
    {
        status.maxHp += modifierStatus.maxHp * value;
        status.maxMp += modifierStatus.maxMp * value;

        status.attackSpeed += modifierStatus.attackSpeed * value;
        status.moveSpeed += modifierStatus.moveSpeed * value;

        SetNowNormalStatus();
    }

    private void SetAttackStatus(AttackStatus status, AttackStatus modifierStatus, int value)
    {
        status.magicCriticalProbability += modifierStatus.magicCriticalProbability * value;
        status.physicalDamage += modifierStatus.physicalDamage * value;
        status.magicDamage += modifierStatus.magicDamage * value;

        status.physicalCriticalDamage += modifierStatus.physicalCriticalDamage * value;
        status.magicCriticalDamage += modifierStatus.magicCriticalDamage * value;

        status.physicalCriticalProbability += modifierStatus.physicalCriticalProbability * value;
        status.magicCriticalProbability += modifierStatus.magicCriticalProbability * value;

        status.accuracy += modifierStatus.accuracy * value;

        status.fireDamageBonus += modifierStatus.fireDamageBonus * value;
        status.waterDamageBonus += modifierStatus.waterDamageBonus * value;
        status.airDamageBonus += modifierStatus.airDamageBonus * value;
        status.earthDamageBonus += modifierStatus.earthDamageBonus * value;
        status.lightDamageBonus += modifierStatus.lightDamageBonus * value;
        status.darkDamageBonus += modifierStatus.darkDamageBonus * value;

        SetNowAttackStatus();
    }

    private void SetDefenceStatus(DefenceStatus status, DefenceStatus modifierStatus, int value)
    {
        status.physicalDefence += modifierStatus.physicalDefence * value;
        status.magicDefence += modifierStatus.magicDefence * value;
        
        status.physicalCriticalResistance += modifierStatus.physicalCriticalResistance * value;
        status.magicCriticalResistance += modifierStatus.magicCriticalResistance * value;
        
        status.dodgeProbability += modifierStatus.dodgeProbability * value;
        
        status.fireResistanceBonus += modifierStatus.fireResistanceBonus * value;
        status.waterResistanceBonus += modifierStatus.waterResistanceBonus * value;
        status.airResistanceBonus += modifierStatus.airResistanceBonus * value;
        status.earthResistanceBonus += modifierStatus.earthResistanceBonus * value;
        
        status.lightResistanceBonus += modifierStatus.lightResistanceBonus * value;
        status.darkResistanceBonus += modifierStatus.darkResistanceBonus * value;

        SetNowDefenceStatus();
    }
    #endregion

    #region ◇◇ 스테이터스 연산 ◇◇
    private NormalStatus defaultNormalStatus;
    private AttackStatus defaultAttackStatus;
    private DefenceStatus defaultDefenceStatus;

    private readonly NormalStatus normalModifier = new();
    private readonly AttackStatus attackModifier = new();
    private readonly DefenceStatus defenceModifier = new();

    private readonly NormalStatus normalModifierMultiply = new();
    private readonly AttackStatus attackModifierMultiply = new();
    private readonly DefenceStatus defenceModifierMultiply = new();

    private void SetNowNormalStatus()
    {
        nowNormalStatus.maxHp = StatusCalc(defaultNormalStatus.maxHp, normalModifier.maxHp, normalModifierMultiply.maxHp);
        nowNormalStatus.maxMp = StatusCalc(defaultNormalStatus.maxMp, normalModifier.maxMp, normalModifierMultiply.maxMp);

        nowNormalStatus.attackSpeed = StatusCalc(defaultNormalStatus.attackSpeed, normalModifier.attackSpeed, normalModifierMultiply.attackSpeed);
        nowNormalStatus.moveSpeed = StatusCalc(defaultNormalStatus.moveSpeed, normalModifier.moveSpeed, normalModifierMultiply.moveSpeed);
    }

    private void SetNowAttackStatus()
    {
        nowAttackStatus.physicalDamage = StatusCalc(defaultAttackStatus.physicalDamage, attackModifier.physicalDamage, attackModifierMultiply.physicalDamage);
        nowAttackStatus.magicDamage = StatusCalc(defaultAttackStatus.magicDamage, attackModifier.magicDamage, attackModifierMultiply.magicDamage);

        nowAttackStatus.physicalCriticalDamage = StatusCalc(defaultAttackStatus.physicalCriticalDamage, attackModifier.physicalCriticalDamage, attackModifierMultiply.physicalCriticalDamage);
        nowAttackStatus.magicCriticalDamage = StatusCalc(defaultAttackStatus.magicCriticalDamage, attackModifier.magicCriticalDamage, attackModifierMultiply.magicCriticalDamage);

        nowAttackStatus.physicalCriticalProbability = StatusCalc(defaultAttackStatus.physicalCriticalProbability, attackModifier.physicalCriticalProbability, attackModifierMultiply.physicalCriticalProbability);
        nowAttackStatus.magicCriticalProbability = StatusCalc(defaultAttackStatus.magicCriticalProbability, attackModifier.magicCriticalProbability, attackModifierMultiply.magicCriticalProbability);

        nowAttackStatus.accuracy = StatusCalc(defaultAttackStatus.accuracy, attackModifier.accuracy, attackModifierMultiply.accuracy);

        nowAttackStatus.fireDamageBonus = StatusCalc(defaultAttackStatus.fireDamageBonus, attackModifier.fireDamageBonus, attackModifierMultiply.fireDamageBonus);
        nowAttackStatus.waterDamageBonus = StatusCalc(defaultAttackStatus.waterDamageBonus, attackModifier.waterDamageBonus, attackModifierMultiply.waterDamageBonus);
        nowAttackStatus.airDamageBonus = StatusCalc(defaultAttackStatus.airDamageBonus, attackModifier.airDamageBonus, attackModifierMultiply.airDamageBonus);
        nowAttackStatus.earthDamageBonus = StatusCalc(defaultAttackStatus.earthDamageBonus, attackModifier.earthDamageBonus, attackModifierMultiply.earthDamageBonus);
        nowAttackStatus.lightDamageBonus = StatusCalc(defaultAttackStatus.lightDamageBonus, attackModifier.lightDamageBonus, attackModifierMultiply.lightDamageBonus);
        nowAttackStatus.darkDamageBonus = StatusCalc(defaultAttackStatus.darkDamageBonus, attackModifier.darkDamageBonus, attackModifierMultiply.darkDamageBonus);
    }

    private void SetNowDefenceStatus()
    {
        nowDefenceStatus.physicalDefence = StatusCalc(defaultDefenceStatus.physicalDefence, defenceModifier.physicalDefence, defenceModifierMultiply.physicalDefence);
        nowDefenceStatus.magicDefence = StatusCalc(defaultDefenceStatus.magicDefence, defenceModifier.magicDefence, defenceModifierMultiply.magicDefence);

        nowDefenceStatus.physicalCriticalResistance = StatusCalc(defaultDefenceStatus.physicalCriticalResistance, defenceModifier.physicalCriticalResistance, defenceModifierMultiply.physicalCriticalResistance);
        nowDefenceStatus.magicCriticalResistance = StatusCalc(defaultDefenceStatus.magicCriticalResistance, defenceModifier.magicCriticalResistance, defenceModifierMultiply.magicCriticalResistance);

        nowDefenceStatus.dodgeProbability = StatusCalc(defaultDefenceStatus.dodgeProbability, defenceModifier.dodgeProbability, defenceModifierMultiply.dodgeProbability);

        nowDefenceStatus.fireResistanceBonus = StatusCalc(defaultDefenceStatus.fireResistanceBonus, defenceModifier.fireResistanceBonus, defenceModifierMultiply.fireResistanceBonus);
        nowDefenceStatus.waterResistanceBonus = StatusCalc(defaultDefenceStatus.waterResistanceBonus, defenceModifier.waterResistanceBonus, defenceModifierMultiply.waterResistanceBonus);
        nowDefenceStatus.airResistanceBonus = StatusCalc(defaultDefenceStatus.airResistanceBonus, defenceModifier.airResistanceBonus, defenceModifierMultiply.airResistanceBonus);
        nowDefenceStatus.earthResistanceBonus = StatusCalc(defaultDefenceStatus.earthResistanceBonus, defenceModifier.earthResistanceBonus, defenceModifierMultiply.earthResistanceBonus);
        nowDefenceStatus.lightResistanceBonus = StatusCalc(defaultDefenceStatus.lightResistanceBonus, defenceModifier.lightResistanceBonus, defenceModifierMultiply.lightResistanceBonus);
        nowDefenceStatus.darkResistanceBonus = StatusCalc(defaultDefenceStatus.darkResistanceBonus, defenceModifier.darkResistanceBonus, defenceModifierMultiply.darkResistanceBonus);
    }

    private int StatusCalc(int a, int b, int c) => (a + b) * c;

    private float StatusCalc(float a, float b, float c) => (a + b) * c;
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusCalc
{
    private const float DM = 1f;

    public static CallbackValueInfo<DamageType> CalculateFinalDamage(AttackStatus attackStatus, DefenceStatus defenceStatus, float skillCoefficient, float additionalDamage, bool isMagicAttack, string attackElement)
    {
        if (IsDodge(attackStatus.accuracy, defenceStatus.dodgeProbability)) return new CallbackValueInfo<DamageType>(DamageType.Miss, 0);

        bool isCriticalHit;
        float baseDamage, damageReductionPercentage, attributeDamagePercentage, criticalDamagePercentage;

        attributeDamagePercentage = CalculateAttributeDamage(attackStatus, defenceStatus, attackElement);

        if (isMagicAttack)
        {
            baseDamage = CalculateBaseDamage(attackStatus.magicDamage, skillCoefficient);
            damageReductionPercentage = CalculateDamageReduction(defenceStatus.magicDefence);
            isCriticalHit = CalculateCriticalHit(attackStatus.magicCriticalProbability, defenceStatus.magicCriticalResistance);

            criticalDamagePercentage = isCriticalHit ? attackStatus.magicCriticalDamage : 100f;
        }
        else
        {
            baseDamage = CalculateBaseDamage(attackStatus.physicalDamage, skillCoefficient);
            damageReductionPercentage = CalculateDamageReduction(defenceStatus.physicalDefence);
            isCriticalHit = CalculateCriticalHit(attackStatus.physicalCriticalProbability, defenceStatus.physicalCriticalResistance);

            criticalDamagePercentage = isCriticalHit ? attackStatus.physicalCriticalDamage : 100f;
        }

        float finalDamage = (baseDamage * damageReductionPercentage * attributeDamagePercentage * (criticalDamagePercentage / 100f)) + additionalDamage;

        return new CallbackValueInfo<DamageType>(isCriticalHit ? DamageType.Critical : DamageType.Normal, finalDamage);
    }

    private static bool IsDodge(float accuracy, float dodgeProbability)
    {
        float hitChance = accuracy - dodgeProbability;

        hitChance = Mathf.Clamp(hitChance, 0f, 100f);

        return Random.Range(1, 101) > hitChance;
    }

    private static float CalculateBaseDamage(int damage, float skillCoefficient)
    {
        return damage * (skillCoefficient / 100f);
    }

    private static float CalculateDamageReduction(float defence)
    {
        return defence / (defence + DM);
    }

    private static float CalculateAttributeDamage(AttackStatus attackStatus, DefenceStatus defenceStatus, string attackElement)
    {
        float damageBonus = 0f;
        float resistanceBonus = 0f;

        switch (attackElement)
        {
            case "fire":
                damageBonus = attackStatus.fireDamageBonus;
                resistanceBonus = defenceStatus.fireResistanceBonus;
                break;

            case "water":
                damageBonus = attackStatus.waterDamageBonus;
                resistanceBonus = defenceStatus.waterResistanceBonus;
                break;

            case "air":
                damageBonus = attackStatus.airDamageBonus;
                resistanceBonus = defenceStatus.airResistanceBonus;
                break;

            case "earth":
                damageBonus = attackStatus.earthDamageBonus;
                resistanceBonus = defenceStatus.earthResistanceBonus;
                break;

            case "light":
                damageBonus = attackStatus.lightDamageBonus;
                resistanceBonus = defenceStatus.lightResistanceBonus;
                break;

            case "dark":
                damageBonus = attackStatus.darkDamageBonus;
                resistanceBonus = defenceStatus.darkResistanceBonus;
                break;

            default:
                break;
        }

        return (1 + damageBonus / 100f) / (1 + resistanceBonus / 100f);
    }

    private static bool CalculateCriticalHit(float criticalProbability, float criticalResistance)
    {
        float criticalChance = criticalProbability - criticalResistance;

        criticalChance = Mathf.Clamp(criticalChance, 0f, 100f);

        return Random.Range(1, 101) <= criticalChance;
    }

    public static int GetHeal(int healAmount)
    {
        if (healAmount < 0) return 0;

        return healAmount;
    }
}

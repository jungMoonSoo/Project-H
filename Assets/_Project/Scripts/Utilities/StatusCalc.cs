using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusCalc
{
    private const float DM = 1f;

    public static CallbackValueInfo<DamageType> CalculateFinalDamage(AttackStatus _attackStatus, DefenceStatus _defenceStatus, float _skillCoefficient, float _additionalDamage, bool _isMagicAttack, string _attackElement)
    {
        if (IsDodge(_attackStatus.accuracy, _defenceStatus.dodgeProbability)) return new CallbackValueInfo<DamageType>(DamageType.Miss, 0);

        bool _isCriticalHit;
        float _baseDamage, _damageReductionPercentage, _attributeDamagePercentage, _criticalDamagePercentage;

        _attributeDamagePercentage = CalculateAttributeDamage(_attackStatus, _defenceStatus, _attackElement);

        if (_isMagicAttack)
        {
            _baseDamage = CalculateBaseDamage(_attackStatus.magicDamage, _skillCoefficient);
            _damageReductionPercentage = CalculateDamageReduction(_defenceStatus.magicDefence);
            _isCriticalHit = CalculateCriticalHit(_attackStatus.magicCriticalProbability, _defenceStatus.magicCriticalResistance);

            _criticalDamagePercentage = _isCriticalHit ? _attackStatus.magicCriticalDamage : 100f;
        }
        else
        {
            _baseDamage = CalculateBaseDamage(_attackStatus.physicalDamage, _skillCoefficient);
            _damageReductionPercentage = CalculateDamageReduction(_defenceStatus.physicalDefence);
            _isCriticalHit = CalculateCriticalHit(_attackStatus.physicalCriticalProbability, _defenceStatus.physicalCriticalResistance);

            _criticalDamagePercentage = _isCriticalHit ? _attackStatus.physicalCriticalDamage : 100f;
        }

        float finalDamage = (_baseDamage * _damageReductionPercentage * _attributeDamagePercentage * (_criticalDamagePercentage / 100f)) + _additionalDamage;

        return new CallbackValueInfo<DamageType>(_isCriticalHit ? DamageType.Critical : DamageType.Normal, (int)finalDamage);
    }

    private static bool IsDodge(float _accuracy, float _dodgeProbability)
    {
        float _hitChance = _accuracy - _dodgeProbability;

        _hitChance = Mathf.Clamp(_hitChance, 0f, 100f);

        return Random.Range(1, 101) > _hitChance;
    }

    private static float CalculateBaseDamage(int _damage, float _skillCoefficient)
    {
        return _damage * (_skillCoefficient / 100f);
    }

    private static float CalculateDamageReduction(float _defence)
    {
        return _defence / (_defence + DM);
    }

    private static float CalculateAttributeDamage(AttackStatus _attackStatus, DefenceStatus _defenceStatus, string _attackElement)
    {
        float _damageBonus = 0f;
        float _resistanceBonus = 0f;

        switch (_attackElement)
        {
            case "fire":
                _damageBonus = _attackStatus.fireDamageBonus;
                _resistanceBonus = _defenceStatus.fireResistanceBonus;
                break;

            case "water":
                _damageBonus = _attackStatus.waterDamageBonus;
                _resistanceBonus = _defenceStatus.waterResistanceBonus;
                break;

            case "air":
                _damageBonus = _attackStatus.airDamageBonus;
                _resistanceBonus = _defenceStatus.airResistanceBonus;
                break;

            case "earth":
                _damageBonus = _attackStatus.earthDamageBonus;
                _resistanceBonus = _defenceStatus.earthResistanceBonus;
                break;

            case "light":
                _damageBonus = _attackStatus.lightDamageBonus;
                _resistanceBonus = _defenceStatus.lightResistanceBonus;
                break;

            case "dark":
                _damageBonus = _attackStatus.darkDamageBonus;
                _resistanceBonus = _defenceStatus.darkResistanceBonus;
                break;

            default:
                break;
        }

        return (1 + _damageBonus / 100f) / (1 + _resistanceBonus / 100f);
    }

    private static bool CalculateCriticalHit(float _criticalProbability, float _criticalResistance)
    {
        float _criticalChance = _criticalProbability - _criticalResistance;

        _criticalChance = Mathf.Clamp(_criticalChance, 0f, 100f);

        return Random.Range(1, 101) <= _criticalChance;
    }

    public static int GetHeal(int _healAmount)
    {
        if (_healAmount < 0) return 0;

        return _healAmount;
    }
}

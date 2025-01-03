using System.Collections.Generic;

public class UnidadModifierHandle
{
    private readonly Unidad unidad;

    private readonly AttackStatus attackStatus;
    private readonly DefenceStatus defenceStatus;

    private readonly AttackStatus attackModifier = new();
    private readonly DefenceStatus defenceModifier = new();

    private readonly AttackStatus attackModifierMultiply = new();
    private readonly DefenceStatus defenceModifierMultiply = new();

    private readonly Dictionary<IUnitModifier, float> unitModifiers = new();

    public UnidadModifierHandle(Unidad unidad, AttackStatus attackStatus, DefenceStatus defenceStatus)
    {
        this.unidad = unidad;

        this.attackStatus = attackStatus;
        this.defenceStatus = defenceStatus;

        InitAttackStatus(attackModifier, 0);
        InitAttackStatus(attackModifierMultiply, 1);

        InitDefenceStatus(defenceModifier, 0);
        InitDefenceStatus(defenceModifierMultiply, 1);
    }

    public void AddModifier(IUnitModifier modifier)
    {
        if (unitModifiers.ContainsKey(modifier)) unitModifiers[modifier] = modifier.Count;
        else unitModifiers.Add(modifier, modifier.Count);

        modifier.Add(unidad);
    }

    public void CheckTick(IUnitModifier modifier, float count)
    {
        modifier.Tick(unidad);

        if (count > unitModifiers[modifier]) RemoveModifier(modifier);
    }

    public void RemoveModifier(IUnitModifier modifier)
    {
        if (unitModifiers.ContainsKey(modifier)) unitModifiers.Remove(modifier);

        modifier.Remove(unidad);
    }

    #region ◇◇ 스테이터스 적용 ◇◇
    public void SetModifier(AttackStatus modifierStatus, bool apply) => SetAttackStatus(attackModifier, modifierStatus, apply ? 1 : -1);
    public void SetModifier(DefenceStatus modifierStatus, bool apply) => SetDefenceStatus(defenceModifier, modifierStatus, apply ? 1 : -1);

    public void SetModifierMultiply(AttackStatus modifierStatus, bool apply) => SetAttackStatus(attackModifierMultiply, modifierStatus, apply ? 1 : -1);
    public void SetModifierMultiply(DefenceStatus modifierStatus, bool apply) => SetDefenceStatus(defenceModifierMultiply, modifierStatus, apply ? 1 : -1);

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
    }
    #endregion

    #region ◇◇ 스테이터스 연산 ◇◇

    #region ◇◇ 공격 스테이터스 반환 ◇◇
    public int PhysicalDamage => StatusCalc(attackStatus.physicalDamage, attackModifier.physicalDamage, attackModifierMultiply.physicalDamage);
    public int MagicDamage => StatusCalc(attackStatus.magicDamage, attackModifier.magicDamage, attackModifierMultiply.magicDamage);

    public float PhysicalCriticalDamage => StatusCalc(attackStatus.physicalCriticalDamage, attackModifier.physicalCriticalDamage, attackModifierMultiply.physicalCriticalDamage);
    public float MagicCriticalDamage => StatusCalc(attackStatus.magicCriticalDamage, attackModifier.magicCriticalDamage, attackModifierMultiply.magicCriticalDamage);

    public float PhysicalCriticalProbability => StatusCalc(attackStatus.physicalCriticalProbability, attackModifier.physicalCriticalProbability, attackModifierMultiply.physicalCriticalProbability);
    public float MagicCriticalProbability => StatusCalc(attackStatus.magicCriticalProbability, attackModifier.magicCriticalProbability, attackModifierMultiply.magicCriticalProbability);

    public float Accuracy => StatusCalc(attackStatus.accuracy, attackModifier.accuracy, attackModifierMultiply.accuracy);

    public float FireDamageBonus => StatusCalc(attackStatus.fireDamageBonus, attackModifier.fireDamageBonus, attackModifierMultiply.fireDamageBonus);
    public float WaterDamageBonus => StatusCalc(attackStatus.waterDamageBonus, attackModifier.waterDamageBonus, attackModifierMultiply.waterDamageBonus);
    public float AirDamageBonus => StatusCalc(attackStatus.airDamageBonus, attackModifier.airDamageBonus, attackModifierMultiply.airDamageBonus);
    public float EarthDamageBonus => StatusCalc(attackStatus.earthDamageBonus, attackModifier.earthDamageBonus, attackModifierMultiply.earthDamageBonus);
    public float LightDamageBonus => StatusCalc(attackStatus.lightDamageBonus, attackModifier.lightDamageBonus, attackModifierMultiply.lightDamageBonus);
    public float DarkDamageBonus => StatusCalc(attackStatus.darkDamageBonus, attackModifier.darkDamageBonus, attackModifierMultiply.darkDamageBonus);
    #endregion

    #region ◇◇ 방어 스테이터스 반환 ◇◇
    public int PhysicalDefence => StatusCalc(defenceStatus.physicalDefence, defenceModifier.physicalDefence, defenceModifierMultiply.physicalDefence);
    public int MagicDefence => StatusCalc(defenceStatus.magicDefence, defenceModifier.magicDefence, defenceModifierMultiply.magicDefence);

    public float PhysicalCriticalResistance => StatusCalc(defenceStatus.physicalCriticalResistance, defenceModifier.physicalCriticalResistance, defenceModifierMultiply.physicalCriticalResistance);
    public float MagicCriticalResistance => StatusCalc(defenceStatus.magicCriticalResistance, defenceModifier.magicCriticalResistance, defenceModifierMultiply.magicCriticalResistance);

    public float DodgeProbability => StatusCalc(defenceStatus.dodgeProbability, defenceModifier.dodgeProbability, defenceModifierMultiply.dodgeProbability);

    public float FireResistanceBonus => StatusCalc(defenceStatus.fireResistanceBonus, defenceModifier.fireResistanceBonus, defenceModifierMultiply.fireResistanceBonus);
    public float WaterResistanceBonus => StatusCalc(defenceStatus.waterResistanceBonus, defenceModifier.waterResistanceBonus, defenceModifierMultiply.waterResistanceBonus);
    public float AirResistanceBonus => StatusCalc(defenceStatus.airResistanceBonus, defenceModifier.airResistanceBonus, defenceModifierMultiply.airResistanceBonus);
    public float EarthResistanceBonus => StatusCalc(defenceStatus.earthResistanceBonus, defenceModifier.earthResistanceBonus, defenceModifierMultiply.earthResistanceBonus);
    public float LightResistanceBonus => StatusCalc(defenceStatus.lightResistanceBonus, defenceModifier.lightResistanceBonus, defenceModifierMultiply.lightResistanceBonus);
    public float DarkResistanceBonus => StatusCalc(defenceStatus.darkResistanceBonus, defenceModifier.darkResistanceBonus, defenceModifierMultiply.darkResistanceBonus);
    #endregion

    private int StatusCalc(int a, int b, int c) => (a + b) * c;

    private float StatusCalc(float a, float b, float c) => (a + b) * c;
    #endregion
}

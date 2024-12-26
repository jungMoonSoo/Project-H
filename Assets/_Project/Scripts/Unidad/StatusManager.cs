using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusManager
{
    private Unidad unidad = null;

    [SerializeField] public BindData<int> hp = new();
    [SerializeField] public BindData<int> mp = new();

    private UnidadStatusBar StatusBar => unidad.statusBar;

    #region ◇ Properties ◇
    private UnidadStatus Status => unidad.Status;
    
    #region ◇◇ 기타 스테이터스 ◇◇
    public int MaxHp => Status.maxHp;
    public int MaxMp => Status.maxMp;
    public float MoveSpeed => Status.moveSpeed;
    public float AttackSpeed => Status.attackSpeed;
    #endregion
    
    #region ◇◇ 공격 스테이터스 ◇◇
    public int PhysicalDamage => Status.attackStatus.physicalDamage + AttackUnitModifier.physicalDamage;
    public int MagicDamage => Status.attackStatus.magicDamage + AttackUnitModifier.magicDamage;
    
    public float PhysicalCriticalDamage => Status.attackStatus.physicalCriticalDamage + AttackUnitModifier.physicalCriticalDamage;
    public float MagicCriticalDamage => Status.attackStatus.magicCriticalDamage + AttackUnitModifier.magicCriticalDamage;
    
    public float PhysicalCriticalProbability => Status.attackStatus.physicalCriticalProbability + AttackUnitModifier.physicalCriticalProbability;
    public float MagicCriticalProbability => Status.attackStatus.magicCriticalProbability + AttackUnitModifier.magicCriticalProbability;
    
    public float Accuracy => Status.attackStatus.accuracy + AttackUnitModifier.accuracy;
    
    public float FireDamageBonus => Status.attackStatus.fireDamageBonus + AttackUnitModifier.fireDamageBonus;
    public float WaterDamageBonus => Status.attackStatus.waterDamageBonus + AttackUnitModifier.waterDamageBonus;
    public float AirDamageBonus => Status.attackStatus.airDamageBonus + AttackUnitModifier.airDamageBonus;
    public float EarthDamageBonus => Status.attackStatus.earthDamageBonus + AttackUnitModifier.earthDamageBonus;
    public float LightDamageBonus => Status.attackStatus.lightDamageBonus + AttackUnitModifier.lightDamageBonus;
    public float DarkDamageBonus => Status.attackStatus.darkDamageBonus + AttackUnitModifier.darkDamageBonus;
    
    public AttackStatus AttackStatus
    {
        get
        {
            attackStatus.physicalDamage = PhysicalDamage;
            attackStatus.magicDamage = MagicDamage;
        
            attackStatus.physicalCriticalDamage = PhysicalCriticalDamage;
            attackStatus.magicCriticalDamage = MagicCriticalDamage;
        
            attackStatus.physicalCriticalProbability = PhysicalCriticalProbability;
            attackStatus.magicCriticalProbability = MagicCriticalProbability;
        
            attackStatus.accuracy = Accuracy;
        
            attackStatus.fireDamageBonus = FireDamageBonus;
            attackStatus.waterDamageBonus = WaterDamageBonus;
            attackStatus.airDamageBonus = AirDamageBonus;
            attackStatus.earthDamageBonus = EarthDamageBonus;
            attackStatus.lightDamageBonus = LightDamageBonus;
            attackStatus.darkDamageBonus = DarkDamageBonus;
        
        
            return attackStatus;
        }
    }
    #endregion
    
    #region ◇◇ 방어 스테이터스 ◇◇
    public int PhysicalDefence => Status.defenceStatus.physicalDefence + DefenceUnitModifier.physicalDefence;
    public int MagicDefence => Status.defenceStatus.magicDefence + DefenceUnitModifier.magicDefence;

    public float PhysicalCriticalResistance => Status.defenceStatus.physicalCriticalResistance + DefenceUnitModifier.physicalCriticalResistance;
    public float MagicCriticalResistance => Status.defenceStatus.magicCriticalResistance + DefenceUnitModifier.magicCriticalResistance;

    public float DodgeProbability => Status.defenceStatus.dodgeProbability + DefenceUnitModifier.dodgeProbability;

    public float FireResistanceBonus => Status.defenceStatus.fireResistanceBonus + DefenceUnitModifier.fireResistanceBonus;
    public float WaterResistanceBonus => Status.defenceStatus.waterResistanceBonus + DefenceUnitModifier.waterResistanceBonus;
    public float AirResistanceBonus => Status.defenceStatus.airResistanceBonus + DefenceUnitModifier.airResistanceBonus;
    public float EarthResistanceBonus => Status.defenceStatus.earthResistanceBonus + DefenceUnitModifier.earthResistanceBonus;
    
    public float LightResistanceBonus => Status.defenceStatus.lightResistanceBonus + DefenceUnitModifier.lightResistanceBonus;
    public float DarkResistanceBonus => Status.defenceStatus.darkResistanceBonus + DefenceUnitModifier.darkResistanceBonus;
    
    public DefenceStatus DefenceStatus
    {
        get
        {
            defenceStatus.physicalDefence = PhysicalDefence;
            defenceStatus.magicDefence = MagicDefence;
            
            defenceStatus.physicalCriticalResistance = PhysicalCriticalResistance;
            defenceStatus.magicCriticalResistance = MagicCriticalResistance;
            
            defenceStatus.dodgeProbability = DodgeProbability;
            
            defenceStatus.fireResistanceBonus = FireResistanceBonus;
            defenceStatus.waterResistanceBonus = WaterResistanceBonus;
            defenceStatus.airResistanceBonus = AirResistanceBonus;
            defenceStatus.earthResistanceBonus = EarthResistanceBonus;
            defenceStatus.lightResistanceBonus = LightResistanceBonus;
            defenceStatus.darkResistanceBonus = DarkResistanceBonus;
            
            
            return defenceStatus;
        }
    }
    #endregion

    #region ◇◇ 변동 스테이터스 ◇◇
    public AttackStatus AttackUnitModifier => attackUnitModifier;
    public DefenceStatus DefenceUnitModifier => defenceUnitModifier;

    public Dictionary<IUnitModifier, float> UnitModifiers => unitModifiers;

    private readonly AttackStatus attackUnitModifier = new();
    private readonly DefenceStatus defenceUnitModifier = new();

    private readonly Dictionary<IUnitModifier, float> unitModifiers = new();
    #endregion

    #endregion

    public Action DieEvent = null;
    private AttackStatus attackStatus = new();
    private DefenceStatus defenceStatus = new();

    public StatusManager(Unidad unidad)
    {
        this.unidad = unidad;

        hp.SetCallback(BindHp);
        mp.SetCallback(BindMp);

        hp.Value = MaxHp;
    }
    
    public void OnDamage(int damage)
    {
        hp.Value -= damage;

        if (hp.Value <= 0)
        {
            DieEvent?.Invoke();
        }
    }

    public void OnHeal(int heal)
    {
        hp.Value += heal;

        if (hp.Value > MaxHp)
        {
            hp.Value = MaxHp;
        }
    }

    private void BindHp(ref int currentValue, int newValue)
    {
        currentValue = newValue;

        if (StatusBar != null) StatusBar.SetBar((float)hp.Value / MaxHp);
    }

    private void BindMp(ref int currentValue, int newValue)
    {
        currentValue = newValue;
    }
}
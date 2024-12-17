using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusManager
{
    private Unidad unidad = null;

    [SerializeField] public int hitpoint;
    [SerializeField] public int mana; 

    #region ◇ Parameters ◇
    private UnidadStatus Status => unidad.Status;
    
    #region ◇◇ 기타 스테이터스 ◇◇
    public int MaxHitPoint => Status.maxHitpoint;
    public int MaxMana => Status.maxMana;
    public float MoveSpeed => Status.moveSpeed;
    public float AttackSpeed => Status.attackSpeed;
    #endregion
    
    #region ◇◇ 공격 스테이터스 ◇◇
    public int PhysicalDamage => Status.attackStatus.physicalDamage + AttackStatusEffect.physicalDamage;
    public int MagicDamage => Status.attackStatus.magicDamage + AttackStatusEffect.magicDamage;
    
    public float PhysicalCriticalDamage => Status.attackStatus.physicalCriticalDamage + AttackStatusEffect.physicalCriticalDamage;
    public float MagicCriticalDamage => Status.attackStatus.magicCriticalDamage + AttackStatusEffect.magicCriticalDamage;
    
    public float PhysicalCriticalProbability => Status.attackStatus.physicalCriticalProbability + AttackStatusEffect.physicalCriticalProbability;
    public float MagicCriticalProbability => Status.attackStatus.magicCriticalProbability + AttackStatusEffect.magicCriticalProbability;
    
    public float Accuracy => Status.attackStatus.accuracy + AttackStatusEffect.accuracy;
    
    public float FireDamageBonus => Status.attackStatus.fireDamageBonus + AttackStatusEffect.fireDamageBonus;
    public float WaterDamageBonus => Status.attackStatus.waterDamageBonus + AttackStatusEffect.waterDamageBonus;
    public float AirDamageBonus => Status.attackStatus.airDamageBonus + AttackStatusEffect.airDamageBonus;
    public float EarthDamageBonus => Status.attackStatus.earthDamageBonus + AttackStatusEffect.earthDamageBonus;
    public float LightDamageBonus => Status.attackStatus.lightDamageBonus + AttackStatusEffect.lightDamageBonus;
    public float DarkDamageBonus => Status.attackStatus.darkDamageBonus + AttackStatusEffect.darkDamageBonus;
    
    public AttackStatus AttackStatus
    {
        get
        {
            attackStatus.physicalDamage = PhysicalDamage;
            attackStatus.magicDamage = MagicDamage;
        
            attackStatus.physicalCriticalDamage = PhysicalCriticalDamage;
            attackStatus.magicCriticalDamage = MagicCriticalDamage;
        
            attackStatus.physicalCriticalProbability = PhysicalCriticalProbability;
            attackStatus.magicCriticalProbability = PhysicalCriticalProbability;
        
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
    public int PhysicalDefence => Status.defenceStatus.physicalDefence + DefenceStatusEffect.physicalDefence;
    public int MagicDefence => Status.defenceStatus.magicDefence + DefenceStatusEffect.magicDefence;

    public float PhysicalCriticalResistance => Status.defenceStatus.physicalCriticalResistance + DefenceStatusEffect.physicalCriticalResistance;
    public float MagicCriticalResistance => Status.defenceStatus.magicCriticalResistance + DefenceStatusEffect.magicCriticalResistance;

    public float DodgeProbability => Status.defenceStatus.dodgeProbability + DefenceStatusEffect.dodgeProbability;

    public float FireResistanceBonus => Status.defenceStatus.fireResistanceBonus + DefenceStatusEffect.fireResistanceBonus;
    public float WaterResistanceBonus => Status.defenceStatus.waterResistanceBonus + DefenceStatusEffect.waterResistanceBonus;
    public float AirResistanceBonus => Status.defenceStatus.airResistanceBonus + DefenceStatusEffect.airResistanceBonus;
    public float EarthResistanceBonus => Status.defenceStatus.earthResistanceBonus + DefenceStatusEffect.earthResistanceBonus;
    
    public float LightResistanceBonus => Status.defenceStatus.lightResistanceBonus + DefenceStatusEffect.lightResistanceBonus;
    public float DarkResistanceBonus => Status.defenceStatus.darkResistanceBonus + DefenceStatusEffect.darkResistanceBonus;
    
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
    #endregion

    public Action DieEvent = null;
    private AttackStatus attackStatus = new();
    private DefenceStatus defenceStatus = new();

    #region ◇◇ 이펙트 스테이터스 ◇◇
    public AttackStatus AttackStatusEffect => attackStatusEffect;
    public DefenceStatus DefenceStatusEffect => defenceStatusEffect;

    public List<IStatusEffect> StatusEffects => statusEffects;

    private readonly AttackStatus attackStatusEffect = new();
    private readonly DefenceStatus defenceStatusEffect = new();

    private readonly List<IStatusEffect> statusEffects = new();
    #endregion


    public StatusManager(Unidad unidad) => this.unidad = unidad;

    
    public void OnDamage(int damage)
    {
        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            DieEvent?.Invoke();
        }
    }

    public void OnHeal(int heal)
    {
        hitpoint += heal;
        if (hitpoint > MaxHitPoint)
        {
            hitpoint = MaxHitPoint;
        }
    }
}
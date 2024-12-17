using System;
using UnityEngine;

[Serializable]
public class StatusManager
{
    private Unidad unidad = null;

    [SerializeField] public int hp;
    [SerializeField] public int mp;

    #region ◇ Parameters ◇
    private UnidadStatus Status => unidad.Status;
    
    #region ◇◇ 기타 스테이터스 ◇◇
    public int MaxHp => Status.maxHp;
    public int MaxMp => Status.maxMp;
    public float MoveSpeed => Status.moveSpeed;
    public float AttackSpeed => Status.attackSpeed;
    #endregion
    
    #region ◇◇ 공격 스테이터스 ◇◇
    public int PhysicalDamage => Status.attackStatus.physicalDamage;
    public int MagicDamage => Status.attackStatus.magicDamage;
    
    public float PhysicalCriticalDamage => Status.attackStatus.physicalCriticalDamage;
    public float MagicCriticalDamage => Status.attackStatus.magicCriticalDamage;
    
    public float PhysicalCriticalProbability => Status.attackStatus.physicalCriticalProbability;
    public float MagicCriticalProbability => Status.attackStatus.magicCriticalProbability;
    
    public float Accuracy => Status.attackStatus.accuracy;
    
    public float FireDamageBonus => Status.attackStatus.fireDamageBonus;
    public float WaterDamageBonus => Status.attackStatus.waterDamageBonus;
    public float AirDamageBonus => Status.attackStatus.airDamageBonus;
    public float EarthDamageBonus => Status.attackStatus.earthDamageBonus;
    public float LightDamageBonus => Status.attackStatus.lightDamageBonus;
    public float DarkDamageBonus => Status.attackStatus.darkDamageBonus;
    
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
    public int PhysicalDefence => Status.defenceStatus.physicalDefence;
    public int MagicDefence => Status.defenceStatus.magicDefence;

    public float PhysicalCriticalResistance => Status.defenceStatus.physicalCriticalResistance;
    public float MagicCriticalResistance => Status.defenceStatus.magicCriticalResistance;

    public float DodgeProbability => Status.defenceStatus.dodgeProbability;

    public float FireResistanceBonus => Status.defenceStatus.fireResistanceBonus;
    public float WaterResistanceBonus => Status.defenceStatus.waterResistanceBonus;
    public float AirResistanceBonus => Status.defenceStatus.airResistanceBonus;
    public float EarthResistanceBonus => Status.defenceStatus.earthResistanceBonus;
    
    public float LightResistanceBonus => Status.defenceStatus.lightResistanceBonus;
    public float DarkResistanceBonus => Status.defenceStatus.darkResistanceBonus;
    
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
    
    
    public StatusManager(Unidad unidad) => this.unidad = unidad;

    
    public void OnDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            DieEvent?.Invoke();
        }
    }

    public void OnHeal(int heal)
    {
        hp += heal;
        if (hp > MaxHp)
        {
            hp = MaxHp;
        }
    }
}
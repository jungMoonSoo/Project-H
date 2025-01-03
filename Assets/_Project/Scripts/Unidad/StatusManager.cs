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

    public UnidadModifierHandle modifierHandle;

    #region ◇◇ 기타 스테이터스 ◇◇
    public int MaxHp => Status.maxHp;
    public int MaxMp => Status.maxMp;
    public float MoveSpeed => Status.moveSpeed;
    public float AttackSpeed => Status.attackSpeed;
    #endregion
    
    #region ◇◇ 공격 스테이터스 ◇◇
    public int PhysicalDamage => modifierHandle.PhysicalDamage;
    public int MagicDamage => modifierHandle.MagicDamage;
    
    public float PhysicalCriticalDamage => modifierHandle.PhysicalCriticalDamage;
    public float MagicCriticalDamage => modifierHandle.MagicCriticalDamage;
    
    public float PhysicalCriticalProbability => modifierHandle.PhysicalCriticalProbability;
    public float MagicCriticalProbability => modifierHandle.MagicCriticalProbability;
    
    public float Accuracy => modifierHandle.Accuracy;
    
    public float FireDamageBonus => modifierHandle.FireDamageBonus;
    public float WaterDamageBonus => modifierHandle.WaterDamageBonus;
    public float AirDamageBonus => modifierHandle.AirDamageBonus;
    public float EarthDamageBonus => modifierHandle.EarthDamageBonus;
    public float LightDamageBonus => modifierHandle.LightDamageBonus;
    public float DarkDamageBonus => modifierHandle.DarkDamageBonus;
    
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
    public int PhysicalDefence => modifierHandle.PhysicalDefence;
    public int MagicDefence => modifierHandle.MagicDefence;

    public float PhysicalCriticalResistance => modifierHandle.PhysicalCriticalResistance;
    public float MagicCriticalResistance => modifierHandle.MagicCriticalResistance;

    public float DodgeProbability => modifierHandle.DodgeProbability;

    public float FireResistanceBonus => modifierHandle.FireResistanceBonus;
    public float WaterResistanceBonus => modifierHandle.WaterResistanceBonus;
    public float AirResistanceBonus => modifierHandle.AirResistanceBonus;
    public float EarthResistanceBonus => modifierHandle.EarthResistanceBonus;
    
    public float LightResistanceBonus => modifierHandle.LightResistanceBonus;
    public float DarkResistanceBonus => modifierHandle.DarkResistanceBonus;
    
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

    public StatusManager(Unidad unidad)
    {
        this.unidad = unidad;

        hp.SetCallback(BindHp);
        mp.SetCallback(BindMp);

        hp.Value = MaxHp;

        modifierHandle = new(unidad, Status.attackStatus, Status.defenceStatus);
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
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusManager
{
    private Unidad unidad = null;

    [SerializeField] public BindData<int> hp = new();
    [SerializeField] public BindData<int> mp = new();

    #region ◇ Properties ◇
    private UnidadStatus Status => unidad.Status;

    public ModifierManager modifierManager;

    #region ◇◇ 기타 스테이터스 ◇◇
    public int MaxHp => Status.maxHp;
    public int MaxMp => Status.maxMp;
    public float MoveSpeed => Status.moveSpeed;
    public float AttackSpeed => Status.attackSpeed;
    #endregion
    
    #region ◇◇ 공격 스테이터스 ◇◇
    public int PhysicalDamage => modifierManager.PhysicalDamage;
    public int MagicDamage => modifierManager.MagicDamage;
    
    public float PhysicalCriticalDamage => modifierManager.PhysicalCriticalDamage;
    public float MagicCriticalDamage => modifierManager.MagicCriticalDamage;
    
    public float PhysicalCriticalProbability => modifierManager.PhysicalCriticalProbability;
    public float MagicCriticalProbability => modifierManager.MagicCriticalProbability;
    
    public float Accuracy => modifierManager.Accuracy;
    
    public float FireDamageBonus => modifierManager.FireDamageBonus;
    public float WaterDamageBonus => modifierManager.WaterDamageBonus;
    public float AirDamageBonus => modifierManager.AirDamageBonus;
    public float EarthDamageBonus => modifierManager.EarthDamageBonus;
    public float LightDamageBonus => modifierManager.LightDamageBonus;
    public float DarkDamageBonus => modifierManager.DarkDamageBonus;
    
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
    public int PhysicalDefence => modifierManager.PhysicalDefence;
    public int MagicDefence => modifierManager.MagicDefence;

    public float PhysicalCriticalResistance => modifierManager.PhysicalCriticalResistance;
    public float MagicCriticalResistance => modifierManager.MagicCriticalResistance;

    public float DodgeProbability => modifierManager.DodgeProbability;

    public float FireResistanceBonus => modifierManager.FireResistanceBonus;
    public float WaterResistanceBonus => modifierManager.WaterResistanceBonus;
    public float AirResistanceBonus => modifierManager.AirResistanceBonus;
    public float EarthResistanceBonus => modifierManager.EarthResistanceBonus;
    
    public float LightResistanceBonus => modifierManager.LightResistanceBonus;
    public float DarkResistanceBonus => modifierManager.DarkResistanceBonus;
    
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

        hp.SetCallback(BindHpStatusBar, SetCallbackType.Add);
        hp.SetCallback(BindHp, SetCallbackType.Set);
        hp.Value = MaxHp;

        modifierManager = new(unidad);

        DieEvent += () => hp.SetCallback(BindHpStatusBar, SetCallbackType.Remove);
    }
    
    public void OnDamage(int damage) => hp.Value -= damage;

    public void OnHeal(int heal) => hp.Value += heal;

    public void BindHpStatusBar(int newValue) => unidad.statusBar?.SetBar((float)newValue / MaxHp);

    public void BindHp(ref int currentValue, int newValue)
    {
        if (newValue <= 0)
        {
            currentValue = 0;
            DieEvent?.Invoke();
        }
        else if (newValue > MaxHp) currentValue = MaxHp;
        else currentValue = newValue;
    }
}
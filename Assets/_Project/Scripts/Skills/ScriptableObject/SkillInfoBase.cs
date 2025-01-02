using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillInfoBase : ScriptableObject
{
    [Header("Skill Info")]
    [SerializeField] public uint code;
    [SerializeField] public string name;
    [SerializeField] public Sprite sprite;
    
    [Header("Target Info")]
    [SerializeField] public TargetType targetType;
    [SerializeField] private TargetingSystemType targetingSystemType;

    #region ◇ Properties ◇
    public ITargetingSystem TargetingSystem
    {
        get
        {
            if (_TargetingSystem is null)
            {
                switch (targetingSystemType)
                {
                    case TargetingSystemType.Single:
                        _TargetingSystem = new SingleTargetingSystem();
                        break;
                    case TargetingSystemType.Linear:
                        _TargetingSystem = new LinearTargetingSystem();
                        break;
                    case TargetingSystemType.Area:
                        _TargetingSystem = new AreaTargetingSystem();
                        break;
                    default:
                        throw new Exception("TargetingSystem Type 미존재.");
                        break;
                }
            }
            
            return _TargetingSystem;
        }
    }
    private ITargetingSystem _TargetingSystem = null;
    #endregion


    /// <summary>
    /// 스킬의 Target이 될 수 있는 Unidad를 반환하는 Method
    /// </summary>
    /// <param name="caster">현재 Target을 찾는 Unidad</param>
    /// <returns>Target이 될 수 있는 Unidad들</returns>
    public List<Unidad> GetTargets(Unidad caster)
    {
        return targetType switch
        {
            TargetType.Me => new List<Unidad>() { caster },
            TargetType.We => UnidadManager.Instance.GetUnidads(caster.Owner, TargetType.We),
            TargetType.They => UnidadManager.Instance.GetUnidads(caster.Owner, TargetType.They)
        };
    }
}

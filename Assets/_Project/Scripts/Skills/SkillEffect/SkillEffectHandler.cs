using System;
using Unity.VisualScripting;
using UnityEngine;

public class SkillEffectHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private TargetingTimingType targetingTimingType;
    [SerializeField] private TargetingSystemType targetingSystemType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;
    [SerializeField] private EllipseCollider effectCollider;


    public Unidad Caster => caster;
    
    
    [NonSerialized] public Unidad[] Targets;

    private Unidad caster;
    private Vector2 position;
    
    private ISkillEffectCreateEvent skillEffectCreateEvent;
    private ISkillEffectTriggerEvent skillEffectTriggerEvent;
    private ISkillEffectFinishEvent skillEffectFinishEvent;
    private ISkillEffectPositioner skillEffectPositioner;
    
    
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


    void Start()
    {
        skillEffectCreateEvent = GetComponent<ISkillEffectCreateEvent>();
        skillEffectTriggerEvent = GetComponent<ISkillEffectTriggerEvent>();
        skillEffectFinishEvent = GetComponent<ISkillEffectFinishEvent>();
        skillEffectPositioner = GetComponent<ISkillEffectPositioner>();
            
            
        skillEffectPositioner?.SetPosition(this, position);
        OnCreate();
        
        animatorEventHandler.OnTriggerEvent = OnTrigger;
        animatorEventHandler.OnFinishEvent = OnFinish;
    }


    public void Init(Unidad caster, Vector2 position)
    {
        this.caster = caster;
        this.position = position;
    }

    
    private void OnCreate()
    {
        if (targetingTimingType == TargetingTimingType.OnCreate)
        {
            Targeting();
        }
        skillEffectCreateEvent?.OnCreate(this);
    }
    private void OnTrigger()
    {
        if (targetingTimingType == TargetingTimingType.OnTrigger)
        {
            Targeting();
        }
        skillEffectTriggerEvent?.OnTrigger(this);
    }
    private void OnFinish()
    {
        skillEffectFinishEvent?.OnFinish(this);
    }

    
    private void Targeting()
    {
        Targets = TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, (Vector2)transform.position + effectCollider.center, effectCollider.size);
    }
}
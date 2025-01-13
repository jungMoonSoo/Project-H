using System;
using UnityEngine;

public class SkillEffectHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private TargetingTimingType targetingTimingType;
    [SerializeField] private TargetingSystemType targetingSystemType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;
    [SerializeField] private EllipseCollider effectCollider;

    private Vector2 position;
    
    private ISkillEffectCreateEvent skillEffectCreateEvent;
    private ISkillEffectTriggerEvent skillEffectTriggerEvent;
    private ISkillEffectFinishEvent skillEffectFinishEvent;
    private ISkillEffectPositioner skillEffectPositioner;

    #region ◇ Properties ◇
    public Unidad Caster { get; private set; }
    public Unidad[] Targets { get; private set; }

    public ITargetingSystem TargetingSystem { get; private set; }
    #endregion

    void Start()
    {
        skillEffectCreateEvent = GetComponent<ISkillEffectCreateEvent>();
        skillEffectTriggerEvent = GetComponent<ISkillEffectTriggerEvent>();
        skillEffectFinishEvent = GetComponent<ISkillEffectFinishEvent>();
        skillEffectPositioner = GetComponent<ISkillEffectPositioner>();

        skillEffectPositioner?.SetPosition(this, position);

        TargetingSystem = SkillTypeHub.GetTargetingSystem(targetingSystemType);

        switch (targetingTimingType)
        {
            case TargetingTimingType.OnCreate:
                OnCreate();
                break;

            case TargetingTimingType.OnTrigger:
                animatorEventHandler.OnTriggerEvent = OnTrigger;
                break;
        }
        
        animatorEventHandler.OnFinishEvent = OnFinish;
    }

    public void Init(Unidad caster, Vector2 position)
    {
        Caster = caster;
        this.position = position;
    }

    private void OnCreate()
    {
        Targeting();
        skillEffectCreateEvent?.OnCreate(this);
    }

    private void OnTrigger()
    {
        Targeting();
        skillEffectTriggerEvent?.OnTrigger(this);
    }

    private void OnFinish()
    {
        skillEffectFinishEvent?.OnFinish(this);
    }

    private void Targeting()
    {
        Targets = TargetingSystem.GetTargets(Caster.Owner, targetType, Caster.transform.position, (Vector2)transform.position + effectCollider.center, effectCollider.size * 0.5f);
    }
}
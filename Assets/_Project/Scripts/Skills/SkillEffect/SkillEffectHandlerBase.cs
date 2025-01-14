using UnityEngine;

public abstract class SkillEffectHandlerBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private TargetingTimingType targetingTimingType;
    [SerializeField] private TargetingSystemType targetingSystemType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;
    
    private ISkillEffectCreateEvent skillEffectCreateEvent;
    private ISkillEffectTriggerEvent skillEffectTriggerEvent;
    private ISkillEffectFinishEvent skillEffectFinishEvent;
    private ISkillEffectPositioner skillEffectPositioner;

    public Unidad Caster { get; private set; }
    public Unidad[] Targets { get; private set; }

    public TargetType TargetType => targetType;
    public ITargetingSystem TargetingSystem { get; private set; }

    public virtual void Init(Unidad caster, Vector2 position)
    {
        Caster = caster;

        skillEffectCreateEvent = GetComponent<ISkillEffectCreateEvent>();
        skillEffectTriggerEvent = GetComponent<ISkillEffectTriggerEvent>();
        skillEffectFinishEvent = GetComponent<ISkillEffectFinishEvent>();
        skillEffectPositioner = GetComponent<ISkillEffectPositioner>();

        skillEffectPositioner?.SetPosition(this, position);

        TargetingSystem = SkillTypeHub.GetTargetingSystem(targetingSystemType);

        switch (targetingTimingType)
        {
            case TargetingTimingType.OnCreate:
                Targets = Targeting(TargetType);
                skillEffectCreateEvent?.OnCreate(this);
                break;

            case TargetingTimingType.OnTrigger:
                animatorEventHandler.OnTriggerEvent = OnTrigger;
                break;
        }

        animatorEventHandler.OnFinishEvent = OnFinish;
    }

    public abstract Vector2 GetAreaSize();

    private void OnTrigger()
    {
        Targets = Targeting(TargetType);
        skillEffectTriggerEvent?.OnTrigger(this);
    }

    private void OnFinish() => skillEffectFinishEvent?.OnFinish(this);

    protected abstract Unidad[] Targeting(TargetType targetType);
}
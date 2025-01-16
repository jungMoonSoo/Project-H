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

    private Unidad Caster
    {
        set
        {
            CastingPosition = value.transform.position;
            Owner = value.Owner;
            AttackStatus = value.NowAttackStatus;
            DefenceStatus = value.NowDefenceStatus;
            NormalStatus = value.NowNormalStatus;
        }
    }
    public Unidad[] Targets { get; private set; }

    public TargetType TargetType => targetType;
    public ITargetingSystem TargetingSystem { get; private set; }


    public Vector2 CastingPosition { get; private set; }
    public UnitType Owner { get; private set; }
    public AttackStatus AttackStatus { get; private set; }
    public DefenceStatus DefenceStatus { get; private set; }
    public NormalStatus NormalStatus { get; private set; }

    
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
                Targets = Targeting();
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
        Targets = Targeting();
        skillEffectTriggerEvent?.OnTrigger(this);
    }

    private void OnFinish() => skillEffectFinishEvent?.OnFinish(this);

    protected abstract Unidad[] Targeting();
}
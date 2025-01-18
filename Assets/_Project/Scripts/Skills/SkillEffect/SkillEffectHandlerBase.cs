using UnityEngine;

public abstract class SkillEffectHandlerBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private TargetingSystemType targetingSystemType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;
    
    private ISkillEffectCreateEvent skillEffectCreateEvent;
    private ISkillEffectTriggerEvent skillEffectTriggerEvent;
    private ISkillEffectFinishEvent skillEffectFinishEvent;
    private ISkillEffectPositioner skillEffectPositioner;

    public Unidad Caster { get; set; }
    public Vector2 CastingPosition { get; private set; }

    private Unidad[] targets;
    public Unidad[] Targets
    {
        get => targets;
        private set => targets = TargetType == TargetType.Me ? new Unidad[] { Caster } : value;
    }

    public TargetType TargetType => targetType;
    public ITargetingSystem TargetingSystem { get; private set; }

    public UnitType Owner => Caster.Owner;
    public NormalStatus NormalStatus => Caster.NowNormalStatus;
    public AttackStatus AttackStatus => Caster.NowAttackStatus;
    public DefenceStatus DefenceStatus => Caster.NowDefenceStatus;

    public virtual void Init(Unidad caster, Vector2 position)
    {
        Caster = caster;
        CastingPosition = caster.transform.position;

        skillEffectCreateEvent = GetComponent<ISkillEffectCreateEvent>();
        skillEffectTriggerEvent = GetComponent<ISkillEffectTriggerEvent>();
        skillEffectFinishEvent = GetComponent<ISkillEffectFinishEvent>();
        skillEffectPositioner = GetComponent<ISkillEffectPositioner>();

        TargetingSystem = SkillTypeHub.GetTargetingSystem(targetingSystemType);

        SetPosition(position);
        OnCreate();

        animatorEventHandler.OnTriggerEvent = OnTrigger;
        animatorEventHandler.OnFinishEvent = OnFinish;
    }

    public abstract Vector2 GetAreaSize();

    protected void SetPosition(Vector2 position) => skillEffectPositioner?.SetPosition(this, position);

    protected void OnCreate()
    {
        Targets = Targeting();
        skillEffectCreateEvent?.OnCreate(this);
    }

    protected void OnTrigger()
    {
        Targets = Targeting();
        skillEffectTriggerEvent?.OnTrigger(this);
    }

    protected void OnFinish() => skillEffectFinishEvent?.OnFinish(this);

    protected abstract Unidad[] Targeting();
}
using UnityEngine;

public abstract class HitObjectBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private RangeType rangeType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;

    private IHitObjectCheckEvent checkEvent;
    private IHitObjectCreateEvent createEvent;
    private IHitObjectTriggerEvent triggerEvent;
    private IHitObjectFinishEvent finishEvent;

    public Unidad Caster { get; set; }
    public Vector3 TargetPos { get; private set; }
    public EffectManager EffectManager { get; private set; }

    private ObjectPool<HitObjectBase> hitObjects;

    public Unidad[] Targets => TargetType == TargetType.Me ? new Unidad[] { Caster } : Targeting();

    public TargetType TargetType => targetType;
    public IRangeTargeting RangeTargeting { get; private set; }

    public void Init(ObjectPool<HitObjectBase> hitObjects) => this.hitObjects = hitObjects;

    public virtual void Init(Unidad caster, EffectManager effectManager, Vector3 createPos)
    {
        Caster = caster;
        EffectManager = effectManager;

        TryGetComponent(out checkEvent);

        checkEvent.HitObject = this;

        createEvent = GetComponent<IHitObjectCreateEvent>();
        triggerEvent = GetComponent<IHitObjectTriggerEvent>();
        finishEvent = GetComponent<IHitObjectFinishEvent>();

        RangeTargeting = SkillTypeHub.GetTargetingSystem(rangeType);

        transform.position = createPos;

        if (animatorEventHandler != null)
        {
            animatorEventHandler.OnTriggerEvent = OnTrigger;
            animatorEventHandler.OnFinishEvent = OnFinish;
        }

        OnCreate();
    }

    private void Update() => checkEvent.Check();

    public void SetTargetPos(Vector3 pos) => TargetPos = pos;

    public void Remove() => hitObjects.Enqueue(this);

    public void OnCreate() => createEvent?.OnCreate(this);

    public void OnTrigger() => triggerEvent?.OnTrigger(this);

    public void OnFinish()
    {
        finishEvent?.OnFinish(this);

        if (hitObjects == null) Destroy(gameObject);
        else Remove();
    }

    public abstract Vector2 GetAreaSize();

    protected abstract Unidad[] Targeting();
}
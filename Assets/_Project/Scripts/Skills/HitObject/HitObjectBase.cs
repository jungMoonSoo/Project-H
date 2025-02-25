using System;
using UnityEngine;

public abstract class HitObjectBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private FilterType filterType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;

    private Action<HitObjectBase> inits;

    private Action<HitObjectBase> checkEvent;
    private Action<HitObjectBase> createEvent;
    private Action<HitObjectBase> triggerEvent;
    private Action<HitObjectBase> finishEvent;

    public Unidad Caster { get; set; }

    public Vector3 CreatePos { get; private set; }
    public Vector3 TargetPos { get; private set; }

    private EffectManager effectManager;
    private ObjectPool<HitObjectBase> hitObjects;

    public abstract Unidad[] Targets { get; }

    public TargetType TargetType => targetType;
    public ITargetingFilter TargetingFilter { get; private set; }

    public void SetPool(ObjectPool<HitObjectBase> hitObjects) => this.hitObjects = hitObjects;

    public virtual void Init(Unidad caster, EffectManager effectManager, Vector3 createPos)
    {
        Caster = caster;
        this.effectManager = effectManager;

        CreatePos = createPos;
        transform.position = createPos;

        if (inits == null)
        {
            foreach (IHitObjectCheckEvent @event in GetComponents<IHitObjectCheckEvent>())
            {
                inits += @event.Init;
                checkEvent += @event.Check;
            }

            foreach (IHitObjectCreateEvent @event in GetComponents<IHitObjectCreateEvent>()) createEvent += @event.OnCreate;

            foreach (IHitObjectTriggerEvent @event in GetComponents<IHitObjectTriggerEvent>())
            {
                inits += @event.Init;
                triggerEvent += @event.OnTrigger;
            }

            foreach (IHitObjectFinishEvent @event in GetComponents<IHitObjectFinishEvent>()) finishEvent += @event.OnFinish;

            if (animatorEventHandler != null)
            {
                animatorEventHandler.OnTriggerEvent = OnTrigger;
                animatorEventHandler.OnFinishEvent = OnFinish;
            }
        }

        TargetingFilter = TargetingFilterHub.GetFilter(filterType);

        inits?.Invoke(this);

        OnCreate();
    }

    private void Update() => checkEvent?.Invoke(this);

    public void SetTargetPos(Vector3 pos) => TargetPos = pos;

    public void OnCreate() => createEvent?.Invoke(this);

    public void OnTrigger() => triggerEvent?.Invoke(this);

    public void OnFinish()
    {
        finishEvent?.Invoke(this);

        if (hitObjects == null) Destroy(gameObject);
        else hitObjects.Enqueue(this);
    }

    public EffectSystem GetEffect(Transform parent)
    {
        if (effectManager == null) return null;

        return effectManager.GetEffect(parent);
    }

    public abstract Vector2 GetAreaSize();
}
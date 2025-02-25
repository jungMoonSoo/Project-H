using System;
using System.Collections.Generic;
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

    public EffectManager EffectManager { get; private set; }

    private ObjectPool<HitObjectBase> hitObjects;

    public Unidad[] Targets => TargetType == TargetType.Me ? new Unidad[] { Caster } : Targeting();

    public TargetType TargetType => targetType;
    public ITargetingFilter TargetingFilter { get; private set; }

    public void Init(ObjectPool<HitObjectBase> hitObjects) => this.hitObjects = hitObjects;

    public virtual void Init(Unidad caster, EffectManager effectManager, Vector3 createPos)
    {
        Caster = caster;
        EffectManager = effectManager;

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
            foreach (IHitObjectTriggerEvent @event in GetComponents<IHitObjectTriggerEvent>()) triggerEvent += @event.OnTrigger;
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

    public abstract Vector2 GetAreaSize();

    protected abstract Unidad[] Targeting();

    protected List<Unidad> GetTargets(UnitType targetOwner, TargetType targetType, ICustomCollider coll)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner, targetType));

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!coll.OnEnter(targets[i].unitCollider)) targets.Remove(targets[i]);
        }

        return targets;
    }
}
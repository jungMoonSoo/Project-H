using System;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private FilterType filterType;

    [SerializeField] private GameObject customColliderObject;

    private bool isFinished;

    private Action<Unidad> inits;

    private Action<HitObject> checkEvent;
    private Action<HitObject> createEvent;
    private Action<HitObject> triggerEvent;
    private Action<HitObject> finishEvent;

    private ICustomCollider customCollider;

    private UnitType owner;

    private Transform target;
    private Vector3 targetPos;

    public Vector3 TargetPos => target == null ? targetPos : target.transform.position;

    private EffectManager effectManager;
    private ObjectPool<HitObject> hitObjects;

    private readonly List<Unidad> targets = new();
    public Unidad[] Targets
    {
        get
        {
            if (TargetType == TargetType.Me) return targets.ToArray();

            targets.Clear();

            foreach (Unidad unidad in UnidadManager.Instance.GetUnidads(owner, TargetType))
            {
                if (customCollider.OnEnter(unidad.unitCollider)) targets.Add(unidad);
            }

            return TargetingFilter.GetFilteredTargets(targets, TargetPos);
        }
    }

    public TargetType TargetType => targetType;
    public ITargetingFilter TargetingFilter { get; private set; }

    public void SetPool(ObjectPool<HitObject> hitObjects) => this.hitObjects = hitObjects;

    public void SetEffect(EffectManager effectManager) => this.effectManager = effectManager;

    public void Init(Unidad caster, Vector3 createPos)
    {
        isFinished = false;

        owner = caster.Owner;

        transform.position = createPos;

        if (inits == null) SetEvents();

        TargetingFilter = TargetingFilterHub.GetFilter(filterType);

        if (TargetType == TargetType.Me) targets.Add(caster);

        inits?.Invoke(caster);

        OnCreate();
    }

    private void Update()
    {
        if (!isFinished) OnCheck();
    }

    public void SetTarget(Vector3 targetPos) => this.targetPos = targetPos;

    public void SetTarget(Transform target) => this.target = target;

    public void OnCheck() => checkEvent?.Invoke(this);

    public void OnCreate() => createEvent?.Invoke(this);

    public void OnTrigger() => triggerEvent?.Invoke(this);

    public void OnFinish()
    {
        isFinished = true;

        if (finishEvent == null) Remove();
        else finishEvent.Invoke(this);
    }

    public EffectSystem GetEffect(Transform parent)
    {
        if (effectManager == null) return null;

        return effectManager.GetEffect(parent);
    }

    public void Remove()
    {
        if (hitObjects == null) Destroy(gameObject);
        else hitObjects.Enqueue(this);
    }

    private void SetEvents()
    {
        customCollider = customColliderObject.GetComponent<ICustomCollider>();

        foreach (IHitObjectCheckEvent @event in GetComponents<IHitObjectCheckEvent>())
        {
            inits += @event.Init;
            checkEvent += @event.Check;
        }

        foreach (IHitObjectCreateEvent @event in GetComponents<IHitObjectCreateEvent>())
        {
            inits += @event.Init;
            createEvent += @event.OnCreate;
        }

        foreach (IHitObjectTriggerEvent @event in GetComponents<IHitObjectTriggerEvent>())
        {
            inits += @event.Init;
            triggerEvent += @event.OnTrigger;
        }

        foreach (IHitObjectFinishEvent @event in GetComponents<IHitObjectFinishEvent>()) finishEvent += @event.OnFinish;
    }
}
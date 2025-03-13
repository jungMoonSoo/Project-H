using System;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private FilterType filterType;

    [SerializeField] private GameObject customColliderObject;
    [SerializeField] private AnimationEventController animController;

    private bool isFinished;

    private Action<HitObject> inits;

    private Action<HitObject> checkEvent;
    private Action<HitObject> createEvent;
    private Action<HitObject> triggerEvent;
    private Action<HitObject> finishEvent;

    private ICustomCollider customCollider;

    private Vector3 targetPos;

    public Unidad Caster { get; set; }
    public Unidad Target { get; set; }

    public Vector3 CreatePos { get; private set; }
    public Vector3 TargetPos => Target == null ? targetPos : Target.boxCollider.GetColliderPos();

    public AnimationEventController AnimController => animController;

    private EffectManager effectManager;
    private ObjectPool<HitObject> hitObjects;

    private readonly List<Unidad> targets = new();

    public Unidad[] Targets
    {
        get
        {
            targets.Clear();

            if (TargetType == TargetType.Me)
            {
                targets.Add(Caster);

                return targets.ToArray();
            }

            foreach (Unidad unidad in UnidadManager.Instance.GetUnidads(Caster.Owner, TargetType))
            {
                if (customCollider.OnEnter(unidad.unitCollider)) targets.Add(unidad);
            }

            return TargetingFilter.GetFilteredTargets(targets, TargetPos);
        }
    }

    public TargetType TargetType => targetType;
    public ITargetingFilter TargetingFilter { get; private set; }

    public void SetPool(ObjectPool<HitObject> hitObjects) => this.hitObjects = hitObjects;

    public virtual void Init(Unidad caster, EffectManager effectManager, Vector3 createPos)
    {
        isFinished = false;

        Caster = caster;
        this.effectManager = effectManager;

        CreatePos = createPos;
        transform.position = createPos;

        if (inits == null)
        {
            customCollider ??= customColliderObject.GetComponent<ICustomCollider>();

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
        }

        TargetingFilter = TargetingFilterHub.GetFilter(filterType);

        inits?.Invoke(this);

        OnCreate();
    }

    private void Update()
    {
        if (!isFinished) checkEvent?.Invoke(this);
    }

    public void SetTargetPos(Vector3 pos) => targetPos = pos;

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

    public Vector2 GetAreaSize()
    {
        customCollider ??= customColliderObject.GetComponent<ICustomCollider>();

        return customCollider.AreaSize;
    }

    public void Remove()
    {
        if (hitObjects == null) Destroy(gameObject);
        else hitObjects.Enqueue(this);
    }
}
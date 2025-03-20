using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetType targetType;
    [SerializeField] private FilterType filterType;

    private bool isFinished;

    private IHitObjectCheckEvent[] checkEvent;
    private IHitObjectTriggerEvent[] triggerEvent;
    private IHitObjectFinishEvent[] finishEvent;

    private ICustomCollider customCollider;

    private UnitType owner;

    private Transform target;
    private Vector3 targetPos;

    public Vector3 TargetPos => target == null ? targetPos : target.transform.position;

    private EffectManager effectManager;
    private IObjectPool<HitObject> hitObjectPool;

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

    public void SetPool(IObjectPool<HitObject> pool) => hitObjectPool = pool;

    public void SetEffect(EffectManager effectManager) => this.effectManager = effectManager;

    public void Init(Unidad caster, Vector3 createPos)
    {
        isFinished = false;

        owner = caster.Owner;

        transform.position = createPos;

        if (customCollider == null) SetEvents();

        TargetingFilter = TargetingFilterHub.GetFilter(filterType);

        targets.Clear();

        if (TargetType == TargetType.Me) targets.Add(caster);

        for (int i = 0; i < checkEvent.Length; i++) checkEvent[i].Init(caster);
        for (int i = 0; i < triggerEvent.Length; i++) triggerEvent[i].Init(caster);
    }

    private void Update()
    {
        if (!isFinished) OnCheck();
    }

    public void SetTarget(Vector3 targetPos) => this.targetPos = targetPos;

    public void SetTarget(Transform target) => this.target = target;

    public void OnCheck()
    {
        for (int i = 0; i < checkEvent.Length; i++) checkEvent[i].OnEvent(this);
    }

    public void OnTrigger()
    {
        for (int i = 0; i < triggerEvent.Length; i++) triggerEvent[i].OnEvent(this);
    }

    public void OnFinish()
    {
        isFinished = true;

        if (finishEvent.Length == 0) Remove();
        else for (int i = 0; i < finishEvent.Length; i++) finishEvent[i].OnEvent(this);
    }

    public EffectSystem GetEffect(Transform parent)
    {
        if (effectManager == null) return null;

        EffectSystem effect = effectManager.EffectPool.Get();

        effect.transform.SetParent(parent);

        return effect;
    }

    public void Remove()
    {
        if (hitObjectPool == null) Destroy(gameObject);
        else hitObjectPool.Release(this);
    }

    private void SetEvents()
    {
        customCollider = GetComponentInChildren<ICustomCollider>();

        checkEvent = GetComponents<IHitObjectCheckEvent>();
        triggerEvent = GetComponents<IHitObjectTriggerEvent>();
        finishEvent = GetComponents<IHitObjectFinishEvent>();
    }
}
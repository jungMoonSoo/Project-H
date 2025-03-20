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

    /// <summary>
    /// 현재 Object의 위치에서 Collider와 충돌한 Target을 Filter로 걸러 반환
    /// </summary>
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

    /// <summary>
    /// 풀링 설정, 설정하지 않는 경우 OnFinish 이후 Object 파괴
    /// </summary>
    /// <param name="pool">풀링 시 사용되는 Interface</param>
    public void SetPool(IObjectPool<HitObject> pool) => hitObjectPool = pool;

    /// <summary>
    /// 공격당한 Target에게 표시되는 Effect 설정, 없는 경우 표시되지 않음
    /// </summary>
    /// <param name="effectManager">풀링용 Manager</param>
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

    /// <summary>
    /// 이동할 위치 지정
    /// </summary>
    /// <param name="targetPos">Object가 이동 할 위치</param>
    public void SetTarget(Vector3 targetPos) => this.targetPos = targetPos;

    /// <summary>
    /// 이동할 대상 지정
    /// </summary>
    /// <param name="target">Object가 이동 할 대상</param>
    public void SetTarget(Transform target) => this.target = target;

    public void OnCheck()
    {
        for (int i = 0; i < checkEvent.Length; i++) checkEvent[i].OnEvent(this);
    }

    public void OnTrigger()
    {
        for (int i = 0; i < triggerEvent.Length; i++) triggerEvent[i].OnEvent(this);
    }

    /// <summary>
    /// 모든 Event 종료 시점에 호출, 
    /// finishEvent가 등록되어 있는 경우 finishEvent에서 Remove 함수를 호출 해야함
    /// </summary>
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
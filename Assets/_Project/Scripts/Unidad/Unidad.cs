using System;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private UnidadStatus status;
    // Unit 종류
    [SerializeField] private UnitType owner;
    
    [Header("States")]
    // State Script가 있는 GameObject들
    [SerializeField] private GameObject idleState;
    [SerializeField] private GameObject moveState;
    [SerializeField] private GameObject attackState;
    [SerializeField] private GameObject skillState;
    [SerializeField] private GameObject dieState;
    [SerializeField] private GameObject readyState;
    [SerializeField] private GameObject stayState;
    [SerializeField] private GameObject pickState;
    
    [Header("Colliders")]
    // Collider Script가 있는 GameObject들
    [SerializeField] public EllipseCollider unitCollider; // 
    [SerializeField] public EllipseCollider attackCollider;
    [SerializeField] public EllipseCollider skillCollider;
    [SerializeField] public BoxColliderManager boxCollider;
    
    [Header("Positions")]
    // Damage UI를 띄울 위치
    [SerializeField] private Transform damageUiPosition; // 
    [SerializeField] private Transform statusUiPosition; // 

    [Header("Pools")]
    [SerializeField] private SpineEffectHandle spineEffectHandle;
    [SerializeField] private HitObjectManager[] hitObjectManagers;

    [Header("Other")]
    [SerializeField] private SpineSkillHandle skillHandle;
    [SerializeField] private Transform view;
    [SerializeField] private Transform body;

    public UnidadAudioHandle audioHandle;
    private UnidadStatusBar statusBar;

    
    public UnitType Owner
    {
        get => owner;
        set
        {
            UnidadManager.Instance.SetUnidad(this, false, owner);
            UnidadManager.Instance.SetUnidad(this, true, value);

            owner = value;
        }
    }

    public UnidadStatus Status => status;

    public BindData<int> Hp => statusManager.hp;

    public NormalStatus NowNormalStatus => ModifierManager.NowNormalStatus;
    public AttackStatus NowAttackStatus => ModifierManager.NowAttackStatus;
    public DefenceStatus NowDefenceStatus => ModifierManager.NowDefenceStatus;

    public ModifierManager ModifierManager => statusManager.modifierManager;

    public Transform DamageUiPosition => damageUiPosition == null ? transform : damageUiPosition;
    public Transform StatusUiPosition => statusUiPosition == null ? transform : statusUiPosition;

    public SpineSkillHandle SkillHandle => skillHandle;
    public Transform View => view;
    public Transform Body => body;

    private StatusManager statusManager = null;

    private Dictionary<UnitState, IUnidadState> states = new();
    private IUnidadState nowState = null;

    public Action DieEvent = null;

    #region ◇ Unity Events ◇
    private void OnDisable()
    {
        UnidadManager.Instance.SetUnidad(this, false, Owner);
    }

    private void Start()
    {
        if (statusManager != null) return;

        Init();
    }

    private void Update() => nowState?.OnUpdate();

    private void OnDestroy()
    {
        SetStatusBar(null);

        UnidadManager.Instance.SetUnidad(this, false, Owner);
    }
    #endregion

    public void Init()
    {
        UnidadManager.Instance.SetUnidad(this, true, Owner);

        statusManager = new StatusManager(this);

        states = new()
        {
            { UnitState.Idle, idleState.GetComponent<IUnidadState>() },
            { UnitState.Move, moveState.GetComponent<IUnidadState>() },
            { UnitState.Attack, attackState.GetComponent<IUnidadState>() },
            { UnitState.Skill, skillState.GetComponent<IUnidadState>() },
            { UnitState.Die, dieState.GetComponent<IUnidadState>() },
            { UnitState.Ready, readyState.GetComponent<IUnidadState>() },
            { UnitState.Stay, stayState.GetComponent<IUnidadState>() },
            { UnitState.Pick, pickState.GetComponent<IUnidadState>() },
        };

        foreach (IUnidadState state in states.Values)
        {
            state.Init();
            state.Unit = this;
        }

        ChangeState(UnitState.Ready);
    }

    public void SetStatusBar(UnidadStatusBar statusBar)
    {
        if (statusBar == null && this.statusBar != null)
        {
            Hp.SetCallback(BindHpStatusBar, SetCallbackType.Remove);

            Destroy(this.statusBar.gameObject);
        }
        else
        {
            this.statusBar = statusBar;

            Hp.SetCallback(BindHpStatusBar, SetCallbackType.Add);
        }
    }

    public void BindHpStatusBar(int newValue) => statusBar.SetBar((float)newValue / NowNormalStatus.maxHp);

    public void ChangeState(UnitState state)
    {
        if (states.TryGetValue(state, out IUnidadState newState))
        {
            nowState?.OnExit();
            nowState = newState;
            nowState.OnEnter();
        }
    }

    public void OnDamage(int damage, DamageType damageTypem, EffectSystem effect = null)
    {
        if (effect != null)
        {
            Vector3 pos = VectorCalc.GetRandomPositionInBoxCollider(boxCollider.Size, Vector2.down, Vector2.one * 0.2f);

            effect.transform.localPosition = pos;
        }

        string text;

        switch (damageTypem)
        {
            case DamageType.Normal:
                text = $"-{damage}";
                break;

            case DamageType.Critical:
                text = $"<size=50%>Critical</size>\n-{damage}";
                break;

            case DamageType.Miss:
                text = "Miss";
                break;

            default:
                text = "";
                break;
        }

        TextPopupManager.Instance.PopupDamage(text, DamageUiPosition.position);

        statusManager?.OnDamage(damage);
    }

    public void OnHeal(int heal, DamageType healType)
    {
        TextPopupManager.Instance.PopupHeal(heal.ToString(), transform.position);

        statusManager?.OnHeal(heal);
    }

    #region ◇ Object Pool ◇
    public void SetEffectPool()
    {
        if (spineEffectHandle == null) return;

        spineEffectHandle.CreateDefaultEffect();
    }

    public void SetHitObjectPool()
    {
        if (hitObjectManagers == null) return;

        foreach (HitObjectManager manager in hitObjectManagers) manager.CreateDefault();
    }
    #endregion

    #region ◇ Modifier ◇
    public void SetLevel(int level) => ModifierManager.SetUnitDefaltStatus(level);
    #endregion
}

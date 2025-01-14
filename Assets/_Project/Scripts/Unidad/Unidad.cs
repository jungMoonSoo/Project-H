using System;
using System.Collections;
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
    [SerializeField] public TouchCollider touchCollider;
    
    [Header("Positions")]
    // Damage UI를 띄울 위치
    [SerializeField] private Transform damageUiPosition; // 
    [SerializeField] private Transform statusUiPosition; // 

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

    [Header("Ather")]
    public UnidadAudioHandle audioHandle;
    private UnidadStatusBar statusBar;

    public UnidadStatus Status => status;

    public BindData<int> Hp => statusManager.hp;
    public BindData<int> Mp => statusManager.mp;

    public NormalStatus NowNormalStatus => statusManager.NormalStatus;
    public AttackStatus NowAttackStatus => statusManager.AttackStatus;
    public DefenceStatus NowDefenceStatus => statusManager.DefenceStatus;

    public ModifierManager ModifierManager => statusManager.modifierManager;

    public Transform DamageUiPosition => damageUiPosition == null ? transform : damageUiPosition;
    public Transform StatusUiPosition => statusUiPosition == null ? transform : statusUiPosition;

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

    private void Update()
    {
        nowState?.OnUpdate();
        CheckModifierCycle();
    }

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

        DieEvent += () => ChangeState(UnitState.Die);
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

    public void OnDamage(int damage, DamageType damageTypem, GameObject effect = null)
    {
        // if (effect != null)
        // {
        //     GameObject instantEffect = Instantiate(effect);
        // 
        //     instantEffect.transform.position = transform.position;
        // }

        TextPopupManager.Instance.PopupDamage(damage.ToString(), DamageUiPosition.position);

        statusManager?.OnDamage(damage);

        IncreaseMp(0.5f);
    }

    public void OnHeal(int heal, DamageType healType)
    {
        TextPopupManager.Instance.PopupHeal(heal.ToString(), transform.position);

        statusManager?.OnHeal(heal);
    }

    public void IncreaseMp(float value) => statusManager.mp.Value += (int)(StatusCalc.MP_REGEN * value);

    public void SetLevel(int level) => ModifierManager.SetUnitDefaltStatus(level);

    #region ◇ Modifier ◇
    public void AddModifier(ModifierBase modifier) => ModifierManager.Add(modifier);

    public void RemoveModifier(ModifierBase modifier) => ModifierManager.Remove(modifier);

    public void ClearModifier() => ModifierManager.Clear();

    public void CheckModifierCycle() => ModifierManager.CheckCycle();
    #endregion
}

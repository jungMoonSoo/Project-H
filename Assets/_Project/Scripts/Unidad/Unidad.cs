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
    public UnidadStatusBar statusBar;

    public UnidadStatus Status => status;
    
    public AttackStatus NowAttackStatus => statusManager.AttackStatus;
    public DefenceStatus NowDefenceStatus => statusManager.DefenceStatus;

    public float MoveSpeed => statusManager.NormalStatus.moveSpeed;
    public ModifierManager ModifierManager => statusManager.modifierManager;

    public Transform DamageUiPosition => damageUiPosition == null ? transform : damageUiPosition;
    public Transform StatusUiPosition => statusUiPosition == null ? transform : statusUiPosition;

    private StatusManager statusManager = null;

    private Dictionary<UnitState, IUnidadState> states = new();
    private IUnidadState nowState = null;

    #region ◇ Unity Events ◇
    void OnDisable()
    {
        UnidadManager.Instance.SetUnidad(this, false, Owner);
    }

    void Start()
    {
        // Scene에 미리 생성된 Unidad를 테스트하기 위해 존재하는 코드
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

        foreach (IUnidadState state in states.Values) state.Unit = this;

        StateChange(UnitState.Stay);
    }

    void Update()
    {
        nowState?.OnUpdate();
        CheckModifierCycle();
    }

    private void OnDestroy()
    {
        if (statusBar != null) Destroy(statusBar.gameObject);

        UnidadManager.Instance.SetUnidad(this, false, Owner);
    }
    #endregion

    public void StateChange(UnitState state)
    {
        if (states.TryGetValue(state, out IUnidadState newState))
        {
            nowState?.OnExit();
            nowState = newState;
            nowState.OnEnter();
        }
    }

    public void OnDamage(int damage, DamageType damageType)
    {
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

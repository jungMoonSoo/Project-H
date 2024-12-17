using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private uint id;
    
    [Header("States")]
    // State Script가 있는 GameObject들
    [SerializeField] private GameObject idleState;
    [SerializeField] private GameObject moveState;
    [SerializeField] private GameObject attackState;
    [SerializeField] private GameObject skillState;
    [SerializeField] private GameObject dieState;
    
    [Header("Colliders")]
    // Collider Script가 있는 GameObject들
    [SerializeField] public EllipseCollider unitCollider; // 
    [SerializeField] public EllipseCollider attackCollider;
    [SerializeField] public EllipseCollider skillCollider;
    
    [Header("Positions")]
    // Damage UI를 띄울 위치
    [SerializeField] private Transform damageUiPosition; // 
    
    [Header("Settings")]
    // Unit 종류
    [SerializeField] public UnitType Owner = UnitType.Enemy;


    public UnidadStatus Status => UnidadManager.Instance.GetStatus(id);
    
    public AttackStatus NowAttackStatus => statusManager.AttackStatus;
    public DefenceStatus NowDefenceStatus => statusManager.DefenceStatus;
    
    public Transform DamageUiPosition => damageUiPosition == null ? transform : damageUiPosition;

    
    private Animator animator = null;
    private IUnidadState nowState = null;
    private StatusManager statusManager = null;
    private Dictionary<UnitState, IUnidadState> states = new();

    #region ◇ Unity Events ◇
    void OnEnable()
    {
        UnidadManager.Instance.unidades.Add(this);
    }

    void OnDisable()
    {
        UnidadManager.Instance.unidades.Remove(this);
    }
    
    void Start()
    {
        statusManager = new StatusManager(this);
        animator = GetComponentInChildren<Animator>();

        states = new()
        {
            { UnitState.Idle, idleState.GetComponent<IUnidadState>() },
            { UnitState.Move, moveState.GetComponent<IUnidadState>() },
            { UnitState.Attack, attackState.GetComponent<IUnidadState>() },
            { UnitState.Skill, skillState.GetComponent<IUnidadState>() },
            { UnitState.Die, dieState.GetComponent<IUnidadState>() },
        };
        foreach (IUnidadState state in states.Values)
        {
            state.Unit = this;
            state.Animator = animator;
        }
        
        StateChange(UnitState.Idle);
    }

    void Update()
    {
        nowState?.OnUpdate();
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
    }

    public void OnHeal(int heal, DamageType healType)
    {
        TextPopupManager.Instance.PopupHeal(heal.ToString(), transform.position);

        statusManager?.OnHeal(heal);        
    }
}

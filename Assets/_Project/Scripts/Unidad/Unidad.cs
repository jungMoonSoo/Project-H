using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private uint id;
    
    [Header("States")]
    [SerializeField] private GameObject idleState;
    [SerializeField] private GameObject moveState;
    [SerializeField] private GameObject attackState;
    [SerializeField] private GameObject skillState;
    [SerializeField] private GameObject dieState;
    
    [Header("Colliders")]
    [SerializeField] public EllipseCollider unitCollider;
    [SerializeField] public EllipseCollider attackCollider;
    [SerializeField] public EllipseCollider skillCollider;
    
    [Header("Settings")]
    [SerializeField] public UnitType Owner = UnitType.Enemy;


    public UnidadStatus Status => UnidadManager.Instance.GetStatus(id);
    
    public AttackStatus NowAttackStatus => statusManager.AttackStatus;
    public DefenceStatus NowDefenceStatus => statusManager.DefenceStatus;

    
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
        animator = GetComponent<Animator>();

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
        // TODO
        //  대미지 UI 생성은 맞는 쪽에서.
        //  데미지를 입히기 전에 UI를 생성해야 함.
        //  힐과는 달리, 데미지는 유닛이 사라질 수 있기 때문.
        statusManager?.OnDamage(damage);
    }

    public void OnHeal(int heal, DamageType healType)
    {
        // TODO
        //  힐 UI 생성은 받은 쪽에서.
        statusManager?.OnHeal(heal);        
    }
}

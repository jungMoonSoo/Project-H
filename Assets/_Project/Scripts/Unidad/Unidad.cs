using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private GameObject idleState;
    [SerializeField] private GameObject moveState;
    [SerializeField] private GameObject attackState;
    [SerializeField] private GameObject skillState;
    [SerializeField] private GameObject dieState;
    
    [Header("Colliders")]
    [SerializeField] public NewEllipseCollider unitCollider;
    [SerializeField] public NewEllipseCollider attackCollider;
    [SerializeField] public NewEllipseCollider skillCollider;
    
    [Header("Settings")]
    [SerializeField] public UnitType Owner = UnitType.Enemy;

    
    private Animator animator = null;

    private IUnidadState nowState = null;
    
    private Dictionary<UnitState, IUnidadState> states = new();

    #region ◇ Unity Events ◇
    void OnEnable()
    {
        //UnitManager.Instance.units.Add(this);
    }

    void OnDisable()
    {
        //UnitManager.Instance.units.Remove(this);
    }
    
    void Start()
    {
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
}

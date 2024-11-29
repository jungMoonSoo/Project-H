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
    [SerializeField] private NewEllipseCollider attackCollider;
    [SerializeField] private NewEllipseCollider skillCollider;

    public UnitType Owner = UnitType.Enemy;

    private Animator animator = null;
    private IUnidadTargeting unidadTargeting = null;

    private IUnidadState nowState = null;
    private bool hasState = false;
    
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
        unidadTargeting = GetComponent<IUnidadTargeting>();

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
        if (hasState)
        {
            nowState.OnUpdate();
        }
    }
    #endregion

    public void StateChange(UnitState state)
    {
        if (states.TryGetValue(state, out IUnidadState newState))
        {
            hasState = true;

            if (nowState != newState)
            {
                nowState?.OnExit();
                nowState = newState;
                nowState.OnEnter();
            }
        }
    }
}

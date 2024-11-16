using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : IUnitState
{
    private readonly Unit unit;

    private UnitStateBase stateBase;

    public int StateNum { get; private set; }
    public UnitState State { get; private set; }
    public UnitStateBase StateBase => stateBase;
    public Animator Animator { get; }

    public UnitStateManager(Unit _unit, Animator _animator)
    {
        unit = _unit;
        Animator = _animator;

        stateBase = new UnitState_Idle(_unit, stateBase); // 기본 상태 설정
    }

    public void StateChange(UnitState newState, int stateNum = 0)
    {
        if (State == newState) return;

        stateBase?.OnExit();

        State = newState;
        StateNum = stateNum;

        stateBase = newState switch
        {
            UnitState.Idle => new UnitState_Idle(unit, stateBase),
            UnitState.Move => new UnitState_Move(unit, stateBase),
            UnitState.Attack => new UnitState_Attack(unit, stateBase),
            UnitState.Skill => new UnitState_Skill(unit, stateBase),
            UnitState.Die => new UnitState_Die(unit, stateBase),
            _ => stateBase
        };

        stateBase.OnEnter();
    }
}

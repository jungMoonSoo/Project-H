using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitState
{
    public int StateNum { get; }
    public UnitState State { get; }
    public UnitStateBase StateBase { get; }

    public Animator Animator { get; }

    public void StateChange(UnitState _state, int _stateNum = 0);
}

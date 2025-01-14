using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : Singleton<PhaseManager>
{
    [Header("PhaseStates")]
    [SerializeField] private GameObject deployPhase;
    [SerializeField] private GameObject readyPhase;
    [SerializeField] private GameObject runPhase;
    [SerializeField] private GameObject victoryPhase;
    [SerializeField] private GameObject defeatPhase;
    [SerializeField] private GameObject waveClearPhase;

    private Dictionary<PhaseState, IPhaseState> states = null;
    private IPhaseState nowState = null;

    void Start()
    {
        states = new()
        {
            { PhaseState.Deploy, deployPhase.GetComponent<IPhaseState>() },
            { PhaseState.Ready, readyPhase.GetComponent<IPhaseState>() },
            { PhaseState.Run, runPhase.GetComponent<IPhaseState>() },
            { PhaseState.Victory, victoryPhase.GetComponent<IPhaseState>() },
            { PhaseState.Defeat, defeatPhase.GetComponent<IPhaseState>() },
            { PhaseState.WaveClear, waveClearPhase.GetComponent<IPhaseState>() }
        };
        ChangeState(PhaseState.Deploy);
    }

    private void Update()
    {
        nowState?.OnUpdate();
    }

    public void ChangeState(PhaseState state)
    {
        if (states.TryGetValue(state, out IPhaseState newState))
        {
            nowState?.OnExit();
            nowState = newState;
            nowState.OnEnter();
        }
    }
    
    public void ChangeReadyPhase()
    {
        ChangeState(PhaseState.Ready);
    }
    public void ChangeRunPhase()
    {
        ChangeState(PhaseState.Run);
    }

}

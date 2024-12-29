using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : Singleton<PhaseManager>
{
    [Header("PhaseStates")]
    [SerializeField] private GameObject readyPhase;
    [SerializeField] private GameObject runPhase;
    [SerializeField] private GameObject endPhase;
    
    
    private Dictionary<PhaseState, IPhaseState> states = null;
    private IPhaseState nowState = null;

    
    void Start()
    {
        states = new()
        {
            { PhaseState.Ready, readyPhase.GetComponent<IPhaseState>() },
            { PhaseState.Run, runPhase.GetComponent<IPhaseState>() },
            { PhaseState.End, endPhase.GetComponent<IPhaseState>() },
        };
        ChangeState(PhaseState.Ready);
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

    public void GameEntrance() //게임 입장
    {
        ChangeState(PhaseState.Run);
        Debug.Log("[Ui Manager]게임에 입장하셨습니다.");
    }
}

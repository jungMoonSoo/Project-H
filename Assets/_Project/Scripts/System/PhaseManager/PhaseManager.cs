using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : Singleton<PhaseManager>
{
    [Header("PhaseStates")]
    // TODO: readyPhase와 runPhase 사이의 풀링 및 최적화 용 페이즈 필요 
    [SerializeField] private GameObject deployPhase;    // 캐릭터 추가/제외 페이즈
    [SerializeField] private GameObject readyPhase;     // 캐릭터 위치 변경 페이즈
    [SerializeField] private GameObject runPhase;       // 실 전투 페이즈
    [SerializeField] private GameObject victoryPhase;   // 웨이브 전부 승리 페이즈
    [SerializeField] private GameObject defeatPhase;    // 웨이브 진행 중 패배 페이즈
    [SerializeField] private GameObject waveClearPhase; // 웨이브 클리어 후 판정 페이즈

    /// <summary>
    /// 현재 진행중인 웨이브
    /// </summary>
    [NonSerialized] public int Wave = 1;
    
    private Dictionary<PhaseState, IPhaseState> states = null;
    private IPhaseState nowState = null;

    
    #region ◇ Unity Methods ◇
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
    #endregion

    
    /// <summary>
    /// State 전환 메소드
    /// </summary>
    /// <param name="state">전환할 State</param>
    public void ChangeState(PhaseState state)
    {
        if (states.TryGetValue(state, out IPhaseState newState))
        {
            nowState?.OnExit();
            nowState = newState;
            nowState.OnEnter();
        }
    }
    
    /// <summary>
    /// ReadyPhase로 전환
    /// </summary>
    public void ChangeReadyPhase()
    {
        if (AllyUnitDeployment.Instance.CurrentUnitNumber == 0) return;

        ChangeState(PhaseState.Ready);
    }
    /// <summary>
    /// RunPhase로 전환
    /// </summary>
    public void ChangeRunPhase()
    {
        ChangeState(PhaseState.Run);
    }
}

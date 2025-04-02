using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 코드 정돈작업 필요
public class WaveClearPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject gameVictoryObject;
    [SerializeField] private GameObject gameDefeatObject;
    [SerializeField] private GameObject waveClearObject;
    public void OnEnter()
    {
        waveClearObject.SetActive(true);
        PhaseManager.Instance.ChangeState(PhaseState.Ready);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        gameVictoryObject.SetActive(false);
        gameDefeatObject.SetActive(false);
        waveClearObject.SetActive(false);
    }
}

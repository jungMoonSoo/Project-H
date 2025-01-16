using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeveClearPhaseWeState : MonoBehaviour, IPhaseState
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

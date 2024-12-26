using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject readyUiGroup;
    
    
    public void OnEnter()
    {
        readyUiGroup.SetActive(true);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        readyUiGroup.SetActive(false);
    }
}

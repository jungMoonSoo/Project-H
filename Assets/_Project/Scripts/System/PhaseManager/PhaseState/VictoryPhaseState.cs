using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private GameObject gameVictoryObject;
    [SerializeField] private GameObject gameDefeatObject;
    [SerializeField] private GameObject stageClearObject;
    public void OnEnter()
    {
        gameEndObject.SetActive(true);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        gameEndObject.SetActive(false);
    }

    public void GameVictory() //승리 문구 출력 메서드 
    {
        Debug.Log("[Ui Manager]게임에서 승리하셨습니다.");

        gameVictoryObject.SetActive(true);
        gameDefeatObject.SetActive(false);
        stageClearObject.SetActive(false);
    }

    public void WaveClear() //웨이브 클리어 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]해당 웨이브를 클리어 하였습니다.");

        gameVictoryObject.SetActive(false);
        gameDefeatObject.SetActive(false);
        stageClearObject.SetActive(true);
    }
}

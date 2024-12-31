using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject gameEndObject;
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

        gameEndObject.transform.GetChild(0).gameObject.SetActive(true);
        gameEndObject.transform.GetChild(1).gameObject.SetActive(false);
        gameEndObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void WaveClear() //웨이브 클리어 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]해당 웨이브를 클리어 하였습니다.");

        gameEndObject.transform.GetChild(0).gameObject.SetActive(false);
        gameEndObject.transform.GetChild(1).gameObject.SetActive(false);
        gameEndObject.transform.GetChild(2).gameObject.SetActive(true);
    }
}

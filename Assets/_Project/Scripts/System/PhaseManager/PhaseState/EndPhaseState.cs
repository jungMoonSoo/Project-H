using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] GameObject gameEnd;
    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
    //Game (승리 / 패배 / Stage Clear) 관련 함수 
    public void GameVictory() //승리 문구 출력 메서드 
    {
        Debug.Log("[Ui Manager]게임에서 승리하셨습니다.");

        gameEnd.SetActive(true);
        gameEnd.transform.GetChild(0).gameObject.SetActive(true);
        gameEnd.transform.GetChild(1).gameObject.SetActive(false);
        gameEnd.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void GameDefeat() //패배 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]게임에서 패배하셨습니다.");

        gameEnd.SetActive(true);
        gameEnd.transform.GetChild(0).gameObject.SetActive(false);
        gameEnd.transform.GetChild(1).gameObject.SetActive(true);
        gameEnd.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void StageClear() //웨이브 클리어 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]해당 스테이지를 클리어 하였습니다.");

        gameEnd.SetActive(true);
        gameEnd.transform.GetChild(0).gameObject.SetActive(false);
        gameEnd.transform.GetChild(1).gameObject.SetActive(false);
        gameEnd.transform.GetChild(2).gameObject.SetActive(true);
    }

}

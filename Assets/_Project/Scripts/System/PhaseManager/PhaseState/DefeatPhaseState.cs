using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject gameEndObject;
    public void OnEnter()
    {
        gameEndObject.SetActive(true);
        GameDefeat();
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        gameEndObject.SetActive(false);
    }

    public void GameDefeat() //패배 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]게임에서 패배하셨습니다.");

        gameEndObject.transform.GetChild(0).gameObject.SetActive(false);
        gameEndObject.transform.GetChild(1).gameObject.SetActive(true);
        gameEndObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}

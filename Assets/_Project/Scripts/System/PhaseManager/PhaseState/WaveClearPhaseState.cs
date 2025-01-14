using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeveClearPhaseWeState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private GameObject gameVictoryObject;
    [SerializeField] private GameObject gameDefeatObject;
    [SerializeField] private GameObject stageClearObject;
    public void OnEnter()
    {
        gameEndObject.SetActive(true);
        GameClear();
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        gameEndObject.SetActive(false);
    }

    public void GameClear() //패배 문구 출력 메서드
    {
        Debug.Log("[Ui Manager]게임에서 패배하셨습니다.");

        gameVictoryObject.SetActive(false);
        gameDefeatObject.SetActive(false);
        stageClearObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardCoreManager : Singleton<StandardCoreManager>
{
    [Header("Object 연결")]
    [SerializeField] GameObject wave;
    [SerializeField] GameObject gameStartButton;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject option;
    [SerializeField] GameObject gameEnd;

    [Header("Skill prefabs 연결")]
    [SerializeField] GameObject[] skillprefabs;

    //타이머 관련 함수
    float timeInSeconds = 120f;     //초기 타이머 시간(한판에 걸리는 시간)
    bool isTimerRunning = false;    //타이머 실행 여부

    //Button 기능
    public void GameStartButton() //게임이 시작 되도록하는 버튼
    {
        Debug.Log("[Standard Core Manager]게임이 시작되었습니다.");
        gameStartButton.SetActive(false);
        timer.SetActive(true);
        TestSystem.Instance.StartTimer();
    }

    public void OptionButton() //게임 옵션창 
    {
        Debug.Log("[Standard Core Manager] 옵션창이 열렸습니다.");

        option.transform.GetChild(1).gameObject.SetActive(true);
        TestSystem.Instance.StopTimer();
    }

    public void CloseOptionWindowButton() //옵션창 닫기
    {
        Debug.Log("[Standard Core Manager] 옵션창이 닫혔습니다.");

        option.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
        TestSystem.Instance.StartTimer();
    }

    //Timer 관련 함수 
    public void UpdateTimerText(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60); // 남은 분
        int seconds = Mathf.FloorToInt(t % 60); // 남은 초

        // "MM:SS" 형식으로 텍스트 업데이트
        timer.GetComponentInChildren<Text>().text = $"{minutes:D2} : {seconds:D2}";
    }

    //Game (승리 / 패배 / Wave Clear) 관련 함수 
    public void GameVictory() //승리 문구 출력 메서드 
    {
        Debug.Log("[Standard Core Manager]게임에서 승리하셨습니다.");

        gameEnd.SetActive(true);
        gameEnd.transform.GetChild(0).gameObject.SetActive(true);
        gameEnd.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void GameDefeat() //패배 문구 출력 메서드
    {
        Debug.Log("[Standard Core Manager]게임에서 패배하셨습니다.");

        gameEnd.SetActive(true);
        gameEnd.transform.GetChild(0).gameObject.SetActive(false);
        gameEnd.transform.GetChild(1).gameObject.SetActive(true);
    }

    //유닛 스킬 테스트용?
    //유닛 스킬하고 유닛에 대해 나오면 작업?
    
}

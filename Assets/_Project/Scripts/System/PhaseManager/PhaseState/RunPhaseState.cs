using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunPhaseState : MonoBehaviour, IPhaseState
{
    [Header("Object 연결")]
    [SerializeField] private GameObject gameStartButtonObject;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject optionObject;

    //타이머 관련 함수
    private float timeInSeconds = 1f;     //초기 타이머 시간(한판에 걸리는 시간)
    private bool isTimerRunning = false;    //타이머 실행 여부

    public void OnEnter()
    {
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Stay);
        gameStartButtonObject.SetActive(false);
        timerObject.SetActive(true);
        AllyUnitDeploymen.Instance.SkillConnect();
        StartTimer();
    }

    public void OnUpdate()
    {
        if (isTimerRunning) //타이머 관련 Update
        {
            timeInSeconds -= Time.deltaTime;
            UpdateTimerText(timeInSeconds);

            // 시간이 0 이하가 되면 타이머 정지 => 패배 문구
            if (timeInSeconds <= 0)
            {
                timeInSeconds = 0;
                StopTimer();
                PhaseManager.Instance.ChangeState(PhaseState.Defeat);
            }
        }
    }

    public void OnExit()
    {
        ActionSkillManager.Instance.ClearSkillButtons();
        timeInSeconds = 0f;
        UpdateTimerText(timeInSeconds);
        StopTimer();
    }
    public void GameStartButton() //게임이 시작 되도록하는 버튼
    {
        Debug.Log("[Ui Manager]게임이 시작되었습니다.");
        gameStartButtonObject.SetActive(false);
        timerObject.SetActive(true);
    }

    public void OptionButton() //게임 옵션창 
    {
        Debug.Log("[Ui Manager] 옵션창이 열렸습니다.");
        StopTimer();
        optionObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void CloseOptionWindowButton() //옵션창 닫기
    {
        Debug.Log("[Ui Manager] 옵션창이 닫혔습니다.");
        StartTimer();
        optionObject.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    //Timer 관련 함수 
    private void UpdateTimerText(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60); // 남은 분
        int seconds = Mathf.FloorToInt(t % 60); // 남은 초

        // "MM:SS" 형식으로 텍스트 업데이트
        timerObject.GetComponentInChildren<Text>().text = $"{minutes:D2} : {seconds:D2}";
    }
    private void StartTimer()// 타이머 시작
    {
        timeInSeconds += Time.deltaTime;
        isTimerRunning = true;
    }
    private void StopTimer()//타이머 멈추기
    {
        isTimerRunning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class RunPhaseState : MonoBehaviour, IPhaseState
{
    [Header("Object 연결")]
    [SerializeField] GameObject runWindow;
    [SerializeField] GameObject wave;
    [SerializeField] GameObject gameStartButton;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject option;

    //타이머 관련 함수
    float timeInSeconds = 120f;     //초기 타이머 시간(한판에 걸리는 시간)
    bool isTimerRunning = false;    //타이머 실행 여부

    //Hold 관련 변수 
    float pressStartTime;                  //누르기 시작한 시간
    bool isPressing = false;               //누르는 상태 여부 
    const float longPressThreshold = 0.5f; //길게 누르기 판정 시간 
    GameObject targetUnit = null;          //눌린 타일 

    private Vector2 initialTouchPosition; // 터치 시작 위치
    private const float stationaryThreshold = 1.0f; // 이동 허용 범위 (픽셀 단위)
    
    [Header("Information 연결")]
    [SerializeField] GameObject allyInfo;
    [SerializeField] GameObject enemyInfo;
    public void OnEnter()
    {
        runWindow.SetActive(true);
    }

    public void OnUpdate()
    {
        TouchInfo touch = TouchSystem.Instance.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (touch.gameObject != null)
                {
                    targetUnit = touch.gameObject.transform.parent.parent.gameObject;
                    initialTouchPosition = touch.pos;
                    pressStartTime = Time.time;
                    isPressing = true;
                }
                break;
            case TouchPhase.Moved:// 터치가 고정되거나 약간 움직이는 경우
                if (isPressing)
                {
                    float pressDuration = Time.time - pressStartTime;
                    float distance = Vector2.Distance(initialTouchPosition, touch.pos);

                    if (distance > stationaryThreshold) //Hold 취소
                    {
                        Debug.Log("[Ui Manager]터치가 움직였습니다. Hold가 취소되었습니다.");
                        isPressing = false;
                    }

                    if (pressDuration >= longPressThreshold)
                    {
                        HandleLongPress(); // 길게 누르기 처리
                        isPressing = false; // 한 번만 실행되도록 플래그 해제
                    }
                }
                break;
            case TouchPhase.Stationary:
                if (isPressing)
                {
                    float pressDuration = Time.time - pressStartTime;
                    if (pressDuration >= longPressThreshold)
                    {
                        HandleLongPress(); // 길게 누르기 처리
                        isPressing = false; // 한 번만 실행되도록 플래그 해제
                    }
                }
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isPressing = false; // 터치가 끝나면 플래그 초기화
                break;
        }
    }

    public void OnExit()
    {
        runWindow.SetActive(false);
    }
    public void GameStartButton() //게임이 시작 되도록하는 버튼
    {
        Debug.Log("[Ui Manager]게임이 시작되었습니다.");
        gameStartButton.SetActive(false);
        timer.SetActive(true);
        TestSystem.Instance.StartTimer();
    }

    public void OptionButton() //게임 옵션창 
    {
        Debug.Log("[Ui Manager] 옵션창이 열렸습니다.");

        option.transform.GetChild(1).gameObject.SetActive(true);
        TestSystem.Instance.StopTimer();
    }

    public void CloseOptionWindowButton() //옵션창 닫기
    {
        Debug.Log("[Ui Manager] 옵션창이 닫혔습니다.");

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
    private void HandleLongPress()//길게 눌렸을 때 적용되는 UI
    {
        if (targetUnit.GetComponent<Unidad>() != null)
        {
            if (targetUnit.GetComponent<Unidad>().Owner == UnitType.Ally)
            {
                Debug.Log("[Standard Game Star tUI]아군 유닛이 선택되었습니다.");
                allyInfo.gameObject.SetActive(true);
                allyInfo.GetComponentInChildren<Text>().text = targetUnit.GetComponent<Unidad>().Status.name;
            }
            else if (targetUnit.GetComponent<Unidad>().Owner == UnitType.Enemy)
            {
                Debug.Log("[Standard Game Start UI]적군 유닛이 선택되었습니다.");
                enemyInfo.gameObject.SetActive(true);
                enemyInfo.GetComponentInChildren<Text>().text = targetUnit.GetComponent<Unidad>().Status.name;
            }
        }
    }
    public void CloseInfoWindow() //설명창 비활성화
    {
        if (allyInfo.activeSelf)
        {
            allyInfo.gameObject.SetActive(false);
        }
        else if (enemyInfo.activeSelf)
        {
            enemyInfo.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject standardCoreFieldUiObject;
    [SerializeField] private GameObject waveObject;
    [SerializeField] private GameObject unitDeploymentObject;
    [SerializeField] private Text stageText;

    [Header("Information 연결")]
    [SerializeField] private GameObject allyInfoObject;
    [SerializeField] private GameObject enemyInfoObject;

    //스테이지 정보 -를 기준으로 앞 뒤 숫자를 받아옴
    private int frontStageNumber = 0;
    private int backStageNumber = 0;

    //Hold 관련 변수 
    private float pressStartTime;                  //누르기 시작한 시간
    private bool isPressing = false;               //누르는 상태 여부 
    private const float longPressThreshold = 0.5f; //길게 누르기 판정 시간 
    private GameObject targetUnit = null;          //눌린 타일 

    private Vector2 initialTouchPosition; // 터치 시작 위치
    private const float stationaryThreshold = 1.0f; // 이동 허용 범위 (픽셀 단위)

    private bool enableHolding = false;
    public void OnEnter()
    {
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);
        unitDeploymentObject.SetActive(true);
    }

    public void OnUpdate()
    {
        if (enableHolding)
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
    }

    public void OnExit()
    {
        enableHolding = false;
        unitDeploymentObject.SetActive(false);
    }

    public void BackWindowButton() //뒤로 가기 버튼
    {
        Debug.Log("[Ui Manager]뒤로가기 버튼을 눌렸습니다. ");
    }

    public void StageTextUpdate(int front, int back) //Stage 정보 업데이트 함수 
    {
        frontStageNumber = front;
        backStageNumber = back;

        stageText.text = $"{frontStageNumber} - {backStageNumber}";
    }

    public void GameEntrance() //게임 입장
    {
        standardCoreFieldUiObject.SetActive(true);
        unitDeploymentObject.SetActive(false);
        enableHolding = true;
        Debug.Log("[Ui Manager]field로 전환 됩니다.");
    }

    private void HandleLongPress()//길게 눌렸을 때 적용되는 UI
    {
        if (targetUnit.GetComponent<Unidad>() != null)
        {
            if (targetUnit.GetComponent<Unidad>().Owner == UnitType.Ally)
            {
                Debug.Log("[Standard Game Star tUI]아군 유닛이 선택되었습니다.");
                allyInfoObject.gameObject.SetActive(true);
                allyInfoObject.GetComponentInChildren<Text>().text = targetUnit.GetComponent<Unidad>().Status.name;
            }
            else if (targetUnit.GetComponent<Unidad>().Owner == UnitType.Enemy)
            {
                Debug.Log("[Standard Game Start UI]적군 유닛이 선택되었습니다.");
                enemyInfoObject.gameObject.SetActive(true);
                enemyInfoObject.GetComponentInChildren<Text>().text = targetUnit.GetComponent<Unidad>().Status.name;
            }
        }
    }
    public void CloseInfoWindow() //설명창 비활성화
    {
        if (allyInfoObject.activeSelf)
        {
            allyInfoObject.gameObject.SetActive(false);
        }
        else if (enemyInfoObject.activeSelf)
        {
            enemyInfoObject.gameObject.SetActive(false);
        }
    }
}

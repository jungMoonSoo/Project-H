using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject gameCombatUi;
    [SerializeField] private UnidadSpawnManager spawnManager;

    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject gameStartButtonObject;

    [Header("Information 연결")]
    [SerializeField] private GameObject allyInfoObject;
    [SerializeField] private GameObject enemyInfoObject;

    [SerializeField] private Text waveText;


    //Hold 관련 변수 
    private float pressStartTime;                  //누르기 시작한 시간
    private bool isPressing = false;               //누르는 상태 여부 
    private const float longPressThreshold = 0.5f; //길게 누르기 판정 시간 
    private GameObject targetUnit = null;          //눌린 타일 
    [SerializeField] private LayerMask unitLayerMask; //UnitLayerMask

    private Vector2 initialTouchPosition; // 터치 시작 위치
    private const float stationaryThreshold = 1.0f; // 이동 허용 범위 (픽셀 단위)

    public void OnEnter()
    {
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);
        gameCombatUi.SetActive(true);

        UnitDeployManager.Instance.SetAllTileActive(true);
        spawnManager.RedeployUnits();

        timerObject.SetActive(false);
        gameStartButtonObject.SetActive(true);
        WaveTextChange(PhaseManager.Instance.wave);

        //TEST
        spawnManager.Spawn(10000, UnitType.Enemy);
        spawnManager.Spawn(10001, 7, UnitType.Enemy);
        spawnManager.Spawn(10000, new Vector2(10, 10), UnitType.Enemy);
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);
    }

    public void OnUpdate()
    {
        TouchInfo touch = TouchSystem.GetTouch(0, unitLayerMask);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (touch[0] != null)
                {
                    targetUnit = touch[0].transform.parent.parent.gameObject;
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
        gameStartButtonObject.SetActive(false);
        timerObject.SetActive(true);   
        UnitDeployManager.Instance.SetAllTileActive(false);
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
            allyInfoObject.SetActive(false);
        }
        else if (enemyInfoObject.activeSelf)
        {
            enemyInfoObject.SetActive(false);
        }
    }

    public void WaveTextChange(int wave)
    {
        waveText.text = wave.ToString() + "Wave";
    }
}

using UnityEngine;
using UnityEngine.UI;

// TODO: 코드 정돈작업 필요
public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject gameCombatUi;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject startButtonObject;

    [Header("UnidadSpawnManager 연결")]
    [SerializeField] private UnidadSpawnManager spawnManager;

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
        //GameObj 관련
        gameCombatUi.SetActive(true);
        UnitDeployManager.Instance.SetAllTileActive(true);
        startButtonObject.SetActive(true);
        timerObject.SetActive(false);

        if(PhaseManager.Instance.Wave > 1) //Wave clear 하고 다시 Ready로 돌아오면 해야할 작업
        {
            WaveTextChange(PhaseManager.Instance.Wave);
            UnitDeployManager.Instance.SetAllTileActive(true);
            UnitDeployManager.Instance.ReturnUnits();
        }

        //TEST => 적군 소환 
        TestEmenySpanw(PhaseManager.Instance.Wave);

    }
    /// <summary>
    /// Touch 감지
    /// </summary>
    public void OnUpdate()
    {
        TouchInfo touch = TouchSystem.GetTouch(0, unitLayerMask);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (touch[0] != null)
                {
                    targetUnit = touch[0].transform.parent.parent.gameObject;
                    initialTouchPosition = touch.GetPos(0);
                    pressStartTime = Time.time;
                    isPressing = true;
                }
                break;
            case TouchPhase.Moved:// 터치가 고정되거나 약간 움직이는 경우
                if (isPressing)
                {
                    float pressDuration = Time.time - pressStartTime;
                    float distance = Vector2.Distance(initialTouchPosition, touch.GetPos(0));

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
        startButtonObject.SetActive(false);
        timerObject.SetActive(true);   
        UnitDeployManager.Instance.SetAllTileActive(false);
    }

    /// <summary>
    /// 길게 Hold 했을 때 나타나는 UI
    /// </summary>
    private void HandleLongPress()
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


    /// <summary>
    /// 설명창 비활성화
    /// </summary>
    public void CloseInfoWindow() 
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

    /// <summary>
    /// 웨이브 정보 체인지
    /// </summary>
    /// <param name="wave"></param>
    public void WaveTextChange(int wave)
    {
        waveText.text = wave.ToString() + "Wave";
    }

    /// <summary>
    ///  임시로 만들어 놓은 적 스폰 함수 -> 이후 업데이트하면서 wave 데이터를 받아서 스폰하도록 만드는 편이 좋아보임
    ///  => 파일 형태를 잡으면 그에 맞추어 다시 제작
    /// </summary>
    /// <param name="wave"></param>
    void TestEmenySpanw(int wave)
    {
        switch (wave) 
        {
            case 1:
                spawnManager.Spawn(10000, 1, UnitType.Enemy);
                spawnManager.Spawn(10000, 2, UnitType.Enemy);
                spawnManager.Spawn(10000, new Vector3(5, 0, 5), UnitType.Enemy);
                UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);
                break;
            case 2:
                spawnManager.Spawn(10000, 2, UnitType.Enemy);
                spawnManager.Spawn(10001, 7, UnitType.Enemy);
                break;
            case 3:
                spawnManager.Spawn(10000, 2, UnitType.Enemy);
                spawnManager.Spawn(10001, 7, UnitType.Enemy);
                spawnManager.Spawn(10001, 8, UnitType.Enemy);
                break;
        }

        UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);

    }
}

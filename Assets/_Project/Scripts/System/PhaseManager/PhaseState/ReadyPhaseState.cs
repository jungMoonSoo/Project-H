using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    #region ◇변수◇
    [Header("Camera 할당")]
    [SerializeField] Camera mainCamera;

    [Header("Information 연결")]
    [SerializeField] GameObject allyInfo;
    [SerializeField] GameObject enemyInfo;

    [Header("GameObject 연결")]
    [SerializeField] GameObject unitDeploymentManger;
    [SerializeField] GameObject warnningObject;
    [SerializeField] Text stageNumber;
    [SerializeField] Text unitNumber;

    [Header("Unit 관리 메니져")]
    [SerializeField] GameObject managers;

    [Header("Ally Tile Handle")]
    [SerializeField] TileHandle[] tileInfo;

    // 스테이지 정보 -를 기준으로 앞 뒤 숫자를 받아옴
    int frontStageNumber = 0;
    int backStageNumber = 0;

    int currentUnitNumber = 1; //현재 필드에 있는 unit 수
    int maxUnitNumber = 5;     //필드 최대 unit 수

    //임시 버튼
    bool blocksmithSpwan = false;
    bool alchemySpawn = false;
    bool nun2Spawn = false;
    bool cowardly_KinghtSpawn = false;
    bool ta001Spawn = false;

    //Hold 관련 변수 
    float pressStartTime;                  //누르기 시작한 시간
    bool isPressing = false;               //누르는 상태 여부 
    const float longPressThreshold = 0.5f; //길게 누르기 판정 시간 
    GameObject targetUnit = null;          //눌린 타일 

    private Vector2 initialTouchPosition; // 터치 시작 위치
    private const float stationaryThreshold = 1.0f; // 이동 허용 범위 (픽셀 단위)
    #endregion

    public void OnEnter()
    {
        unitDeploymentManger.SetActive(true);
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
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isPressing = false; // 터치가 끝나면 플래그 초기화
                break;
        }
    }

    public void OnExit()
    {
        unitDeploymentManger.SetActive(false);
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

    #region◇System 관련 메서드◇ 
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
    public void BackWindowButton() //뒤로 가기 버튼
    {
        Debug.Log("[Ui Manager]뒤로가기 버튼을 눌렸습니다. ");
    }
    public void StageTextUpdate(int front, int back) //Stage 정보 업데이트 함수 
    {
        frontStageNumber = front;
        backStageNumber = back;

        stageNumber.text = $"{frontStageNumber} - {backStageNumber}";
    }
    public void UnitDeployTextUpdate(int num) //배치 수 업데이트 함수 
    {
        currentUnitNumber = num;
        unitNumber.text = $"배치수 ({currentUnitNumber} / {maxUnitNumber})";
    }

    //코루틴 함수 
    IEnumerator WarningTextPrint() //경고 문구
    {
        warnningObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        warnningObject.SetActive(false);
    }
    #endregion

    #region◇Unidad 배치 메서드◇ 
    public void BlocksmithButton() //대장장이 버튼 
    {
        if (!blocksmithSpwan)
        {
            CreateUnit(0);
            blocksmithSpwan = true;
        }
        else
        {
            DestroyUnit(0);
            blocksmithSpwan = false;

        }
    }

    public void AlchemyButton()//물약이 버튼
    {
        if (!alchemySpawn)
        {
            CreateUnit(1);
            alchemySpawn = true;
        }
        else
        {
            DestroyUnit(1);
            alchemySpawn = false;
        }
    }

    public void Nun2Button() //수녀 버튼
    {
        if (!nun2Spawn)
        {
            CreateUnit(2);
            nun2Spawn = true;
        }
        else
        {
            DestroyUnit(2);
            nun2Spawn = false;
        }
    }

    public void Cowardly_KinghtButton() //갑옷 
    {
        if (!cowardly_KinghtSpawn)
        {
            CreateUnit(3);
            cowardly_KinghtSpawn = true;
        }
        else
        {
            DestroyUnit(3);
            cowardly_KinghtSpawn = false;
        }
    }

    public void TA001Buttton() //번개궁수
    {
        if (!ta001Spawn)
        {
            CreateUnit(4);
            ta001Spawn = true;
        }
        else
        {
            DestroyUnit(4);
            ta001Spawn = false;
        }
    }

    //unit  생성 & Unit 삭제에 대한 메서드 작성

    void CreateUnit(uint num) //Unit 생성 
    {
        if (currentUnitNumber < maxUnitNumber)
        {
            managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(num), 0, true);
            currentUnitNumber++;
            UnitDeployTextUpdate(currentUnitNumber);
        }
        else
        {
            StartCoroutine(WarningTextPrint());
            Debug.Log("[Ui Manager]유닛 배치 최대입니다.");
        }
    }

    void DestroyUnit(uint num) //Unit 삭제 => 삭제 메세드 
    {
        for (int i = 0; i < tileInfo.Length; i++)
        {
            if (tileInfo[i].Unit != null)
            {
                if (tileInfo[i].Unit.Status == UnidadManager.Instance.GetStatus(num))
                {
                    currentUnitNumber -= 1;
                    Destroy(tileInfo[i].Unit.gameObject);
                    tileInfo[i].RemoveUnit();
                    Debug.Log("[Ui Manager]해당 유닛을 제거하였습니다.");
                    break;
                }
            }
        }
    }
    #endregion
}

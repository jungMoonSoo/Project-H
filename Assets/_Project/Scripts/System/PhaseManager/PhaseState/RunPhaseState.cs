using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunPhaseState : MonoBehaviour, IPhaseState
{
    [Header("SetActive Objects")]
    [SerializeField] private GameObject[] enableObjects;
    [SerializeField] private GameObject[] disableObjects;
    
    [Header("Unit Groups")]
    [SerializeField] private GameObject allyGroup;
    [SerializeField] private GameObject enemyGroup;
    
    [Header("Timer")]
    [SerializeField] private Text txtCounter;
    [SerializeField] private float timeInSeconds = 120f;
    
    [Header("Object 연결")]
    [SerializeField] private GameObject optionObject;
    
    
    //타이머 관련 함수
    private float timerCount;     //초기 타이머 시간(한판에 걸리는 시간)
    private bool isTimerRunning = false;    //타이머 실행 여부\
    
    
    public void OnEnter()
    {
        timerCount = timeInSeconds;
        
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Stay);
        AllyUnitDeploymen.Instance.SkillConnect(); // TODO: CreateSkillButton으로 Method명을 변경해야 할 것 같음
        StartTimer();
        
        //TEST
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Idle);

        
        // 사용 및 비사용 오브젝트 Active 처리
        foreach (var obj in enableObjects) obj.SetActive(true);
        foreach (var obj in disableObjects) obj.SetActive(false);
    }

    public void OnUpdate()
    {
        if (isTimerRunning) //타이머 관련 Update
        {
            timerCount -= Time.deltaTime;
            UpdateTimerText(timerCount);

            // 시간이 0 이하가 되면 타이머 정지 => 패배 문구
            if (timerCount <= 0)
            {
                timerCount = 0;
                StopTimer();
                PhaseManager.Instance.ChangeState(PhaseState.Defeat);
            }
        }

        if(allyGroup.transform.childCount == 0)
        {
            PhaseManager.Instance.ChangeState(PhaseState.Defeat);
        }
        else if(enemyGroup.transform.childCount == 0)
        {
            if(PhaseManager.Instance.wave >= 3)
            {
                PhaseManager.Instance.ChangeState(PhaseState.Victory);
            }
            else
            {
                PhaseManager.Instance.ChangeState(PhaseState.WaveClear);
            }
        }
        
    }

    public void OnExit()
    {
        PhaseManager.Instance.wave++;
        ActionSkillManager.Instance.ClearSkillButtons();
        timerCount = 0f;
        UpdateTimerText(timerCount);
        StopTimer();
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
        txtCounter.text = $"{minutes:D2} : {seconds:D2}";
    }
    private void StartTimer()// 타이머 시작
    {
        timerCount += Time.deltaTime;
        isTimerRunning = true;
    }
    private void StopTimer()//타이머 멈추기
    {
        isTimerRunning = false;
    }
}

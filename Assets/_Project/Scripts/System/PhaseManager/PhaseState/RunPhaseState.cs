using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunPhaseState : MonoBehaviour, IPhaseState
{
    //[Header("SetActive Objects")]
    //[SerializeField] private GameObject[] enableObjects;
    //[SerializeField] private GameObject[] disableObjects;
    
    [Header("Unit Groups")]
    [SerializeField] private GameObject allyGroup;
    [SerializeField] private GameObject enemyGroup;
    
    [Header("Timer")]
    [SerializeField] private Text txtCounter;
    [SerializeField] private float timeInSeconds = 120f;
    
    //타이머 관련 함수
    private float timerCount;     //초기 타이머 시간(한판에 걸리는 시간)
    private bool isTimerRunning = false;    //타이머 실행 여부\
    
    
    public void OnEnter()
    {
        timerCount = timeInSeconds;
        
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Stay);
        AllyUnitDeployment.Instance.CreateSkillButton();
        StartTimer();
        
        //TEST
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Idle);

        
        //// 사용 및 비사용 오브젝트 Active 처리
        //foreach (var obj in enableObjects) obj.SetActive(true);
        //foreach (var obj in disableObjects) obj.SetActive(false);
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
            if(PhaseManager.Instance.Wave >= 3)
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
        PhaseManager.Instance.Wave++;
        ActionSkillManager.Instance.OnCancel();
        ActionSkillManager.Instance.ClearSkillButtons();
        timerCount = 0f;
        UpdateTimerText(timerCount);
        StopTimer();
    }

    /// <summary>
    /// Timer 업데이트
    /// </summary>
    /// <param name="t"></param>
    private void UpdateTimerText(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60); // 남은 분
        int seconds = Mathf.FloorToInt(t % 60); // 남은 초

        // "MM:SS" 형식으로 텍스트 업데이트
        txtCounter.text = $"{minutes:D2} : {seconds:D2}";
    }

    /// <summary>
    /// Timer 시작
    /// </summary>
    private void StartTimer()
    {
        timerCount += Time.deltaTime;
        isTimerRunning = true;
    }

    /// <summary>
    /// Timer 멈추기
    /// </summary>
    private void StopTimer()
    {
        isTimerRunning = false;
    }
}

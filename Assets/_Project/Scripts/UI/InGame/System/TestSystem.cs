using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test용 System 코드, UI쪽 Timer 보기 위해 작성
/// </summary>
public class TestSystem : Singleton<TestSystem>
{
    //타이머 관련 함수
    float timeInSeconds = 120f;     //초기 타이머 시간(한판에 걸리는 시간)
    bool isTimerRunning = false;    //타이머 실행 여부
    private void Update()
    {
        if (isTimerRunning) //타이머 관련 Update
        {
            timeInSeconds -= Time.deltaTime;
            StandardCoreManager.Instance.UpdateTimerText(timeInSeconds);
            // 시간이 0 이하가 되면 타이머 정지 => 패배 문구
            if (timeInSeconds <= 0)
            {
                timeInSeconds = 0;
                StandardCoreManager.Instance.GameDefeat();
                StopTimer();
            }
        }
    }

    public void StartTimer()// 타이머 시작
    {
        timeInSeconds += Time.deltaTime;
        isTimerRunning = true;
    }
    public void StopTimer()//타이머 멈추기
    {
        isTimerRunning = false;
    }
}

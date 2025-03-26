using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    private InGameSpeed inGameSpeed = InGameSpeed.Default;
    
    private readonly HashSet<PauseType> pauseList = new();

    private readonly Dictionary<PauseType, float> pauseSpeed = new()
    {
        {PauseType.UseSkill, 0.25f},
        {PauseType.OpenMenu, 0f},
        {PauseType.OpenOption, 0f},
    };


    private float TimeScale => inGameSpeed switch
    {
        InGameSpeed.Default => 1f,
        InGameSpeed.Double => 2f,
    };


    /// <summary>
    /// 게임 일시정지 Method
    /// </summary>
    /// <param name="pauseType">일시정지 사유</param>
    public void PauseGame(PauseType pauseType)
    {
        if(!pauseList.Contains(pauseType))
        {
            pauseList.Add(pauseType);
        }
        CheckPause();
    }

    /// <summary>
    /// 게임 재개 Method
    /// </summary>
    /// <param name="pauseType">일시정지 사유</param>
    public void ResumeGame(PauseType pauseType)
    {
        pauseList.Clear();
        CheckPause();
    }
    /// <summary>
    /// 게임 강제 재개 Method
    /// </summary>
    public void ForcedResumeGame()
    {
        pauseList.Clear();
        CheckPause();
    }

    /// <summary>
    /// 게임 배속 버튼 Method
    /// </summary>
    public void ToggleFastButton()
    {
        inGameSpeed = inGameSpeed switch
        {
            InGameSpeed.Default => InGameSpeed.Double,
            InGameSpeed.Double => InGameSpeed.Default,
            _ => inGameSpeed
        };
        CheckPause();
    }


    /// <summary>
    /// 게임 일시정지 판단 Method
    /// 일시정지 사유가 없으면 게임을 재개한다.
    /// </summary>
    private void CheckPause()
    {
        float timeScale = TimeScale;
        foreach (var pause in pauseList)
        {
            if (pauseSpeed.TryGetValue(pause, out float value))
            {
                timeScale = value;
            }
        }
        
        Time.timeScale = timeScale;
    }

    public void TestStop()
    {
        if(pauseList.Count > 0)
        {
            pauseList.Clear();
        }
        else
        {
            pauseList.Add(PauseType.OpenMenu);
        }
        CheckPause();
    }
}

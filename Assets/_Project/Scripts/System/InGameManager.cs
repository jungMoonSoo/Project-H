using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    public Camera MainCamera;
    public RectTransform SkillGroup;
    private readonly List<PauseType> pauseList = new();

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
        pauseList.RemoveAll(x => x == pauseType);
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
    /// 게임 일시정지 판단 Method
    /// 일시정지 사유가 없으면 게임을 재개한다.
    /// </summary>
    private void CheckPause()
    {
        if(pauseList.Count > 0)
        {
            // TODO 게임 일시정지
            Time.timeScale = 0;
            Logger.Debug("게임 일시정지", "일시정지");
        }
        else
        {
            // TODO 게임 재개
            Time.timeScale = 1;
            Logger.Debug("게임 일시정지", "게임재개");
        }
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

    public void TestSkill()
    {
        /*if(!Action)
        {
            if (skill.SkillRequire.CheckRequire(null))
            {
                Action = true;
                skill.SkillPrepare.Begin(null);
            }
        }
        else
        {
            skill.SkillPrepare.End(null);
        }*/
    }
}

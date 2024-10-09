using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
{
    public virtual void BeginUse()
    {
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
    }

    public virtual void AfterUse()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
    }
}

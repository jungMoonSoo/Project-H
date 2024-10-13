using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBase
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

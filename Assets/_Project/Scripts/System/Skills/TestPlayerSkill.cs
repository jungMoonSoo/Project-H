using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerSkill : PlayerSkillBase, IPlayerSkill
{
    public void Use()
    {
        Logger.Info("테스트 스킬 사용");
    }
}

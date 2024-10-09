using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : SkillBase, ISkill
{
    public void Use()
    {
        Logger.Info("테스트 스킬 사용");
    }
}

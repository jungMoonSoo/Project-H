using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSkills
{
    public List<UnitSkillBase> Skills { get; }

    public bool CheckSkill();
}

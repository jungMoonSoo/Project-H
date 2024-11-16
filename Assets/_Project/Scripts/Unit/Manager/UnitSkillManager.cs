using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkillManager : IUnitSkills
{
    private readonly Unit unit;

    public List<UnitSkillBase> Skills { get; }

    public UnitSkillManager(Unit _unit, List<UnitSkillBase> _skills)
    {
        unit = _unit;
        Skills = _skills;
    }

    public bool CheckSkill()
    {
        if (Skills.Count < unit.StateNum + 1) return false;
        if (unit.Status.mp[0].Data != unit.Status.mp[1].Data) return false;

        return true;
    }
}
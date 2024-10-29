using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill_Heal_Require : MonoBehaviour, ISkillRequire<Unit>
{
    public bool CheckRequire(Unit _unit)
    {

        return true;
    }
}

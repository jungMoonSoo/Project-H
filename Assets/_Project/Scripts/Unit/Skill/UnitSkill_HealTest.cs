using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill_HealTest : SkillBase<Unit, Unit, Unit, Unit, Unit>
{
    private UnitSkill_Heal_Require require = new();
    private UnitSkill_Heal_Prepare prepare = new();
    private UnitSkill_Heal_PreAction preAction = new();
    private UnitSkill_Heal_Invoke invoke = new();
    private UnitSkill_Heal_AfterAction afterAction = new();

    public override ISkillRequire<Unit> SkillRequire => require;
    public override ISkillPrepare<Unit> SkillPrepare => prepare;
    public override ISkillPreAction<Unit> SkillPreAction => preAction;
    public override ISkillInvoke<Unit> SkillInvoke => invoke;
    public override ISkillAfterAction<Unit> SkillAfterAction => afterAction;

    public void Init(Unit _unit)
    {
        _unit.TryGetComponent(out require);
        _unit.TryGetComponent(out prepare);
        _unit.TryGetComponent(out preAction);
        _unit.TryGetComponent(out invoke);
        _unit.TryGetComponent(out afterAction);
    }
}

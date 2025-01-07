using UnityEngine;

[CreateAssetMenu(menuName = "Skill/PassiveSkill", fileName = "NewPassiveSkill")]
public class PassiveSkillInfo: SkillInfoBase
{
    [Header("Passive Skill Info")]
    public PassiveRequireBase passiveRequire;
}
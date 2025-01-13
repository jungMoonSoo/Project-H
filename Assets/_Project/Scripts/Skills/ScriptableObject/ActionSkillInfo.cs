using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActionSkill", fileName = "NewActionSkill")]
public class ActionSkillInfo: SkillInfoBase
{
    [Header("Active Skill Info")]
    [SerializeField] public SkillEffectHandlerBase effectPrefab;
    [SerializeField] public SkillAreaInfo skillArea;
}
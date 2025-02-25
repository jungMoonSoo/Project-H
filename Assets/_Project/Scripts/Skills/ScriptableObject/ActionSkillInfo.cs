using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActionSkill", fileName = "NewActionSkill")]
public class ActionSkillInfo: SkillInfoBase
{
    [Header("Active Skill Info")] [SerializeField]
    public HitObjectBase effectPrefab;
    public float manaCost;
    
    [Header("Area System Info")]
    [SerializeField] public Sprite areaImage;
    [SerializeField] public SkillAreaType skillAreaType;
}
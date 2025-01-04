using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillArea", fileName = "NewSkillArea")]
public class SkillAreaInfo: ScriptableObject
{
    [Header("Area System Info")]
    [SerializeField] public Sprite areaImage;
    [SerializeField] public Vector2 areaSize;
    [SerializeField] private SkillAreaType skillAreaType;

    
    public ISkillArea SkillArea => SkillTypeHub.GetSkillArea(skillAreaType);
}
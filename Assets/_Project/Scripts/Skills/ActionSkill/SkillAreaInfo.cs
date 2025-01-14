using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillArea", fileName = "NewSkillArea")]
public class SkillAreaInfo : ScriptableObject
{
    [Header("Area System Info")]
    [SerializeField] public Sprite areaImage;
    [SerializeField] public TargetType targetType;
    [SerializeField] public SkillAreaType skillAreaType;
}

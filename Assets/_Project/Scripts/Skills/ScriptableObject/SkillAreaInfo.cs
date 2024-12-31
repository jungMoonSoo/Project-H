using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillArea", fileName = "NewSkillArea")]
public class SkillAreaInfo: ScriptableObject
{
    [Header("Area System Info")]
    [SerializeField] public Sprite areaImage;
    [SerializeField] private SkillAreaType skillAreaType;

    #region ◇ Properties ◇
    public ISkillArea SkillArea
    {
        get
        {
            if (_SkillArea is null)
            {
                switch (skillAreaType)
                {
                    case SkillAreaType.Single:
                        _SkillArea = new SingleSkillArea();
                        break;
                    case SkillAreaType.Linear:
                        _SkillArea = new LinearSkillArea();
                        break;
                    case SkillAreaType.Area:
                        _SkillArea = new EllipseSkillArea();
                        break;
                    default:
                        throw new Exception("SkillArea Type 미존재.");
                        break;
                }
            }
            
            return _SkillArea;
        }
    }
    private ISkillArea _SkillArea = null;
    #endregion
}
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultActionSkill: IActionSkill
{
    public Unidad Caster { get; set; }

    public ISkillArea SkillArea
    {
        get => _SKillArea;
        set
        {
            _SKillArea = value;
            _SKillArea.Skill = this;
        }
    }
    private ISkillArea _SKillArea;

    public Vector2 AreaSize
    {
        get;
        set;
    } = new(5, 2.5f);
    

    public void OnSelect()
    {
        
    }

    public void OnDrag(Vector3 worldPosition)
    {
        SkillArea?.SetPosition(worldPosition);
    }

    public void EndAction()
    {

    }

    public void ApplyAction(Vector3 worldPosition)
    {
        
    }
}
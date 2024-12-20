using UnityEngine;

public class DefaultActionSkill: IActionSkill
{
    public Unidad Unit
    {
        get; set;
    }
    public ISkillArea SkillArea { get; } = new EllipseSkillArea();

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
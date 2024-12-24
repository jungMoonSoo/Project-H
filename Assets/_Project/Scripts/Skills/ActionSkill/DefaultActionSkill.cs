using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Cache = UnityEngine.Cache;

public class DefaultActionSkill: IActionSkill
{
    public uint EffectCode { get; set; }
    
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
    
    public Vector2 AreaSize { get; set; } = new(5, 2.5f);
    public Vector2 SkillRange => Caster.skillCollider.size;

    public ISkillEffect SkillEffect;
    

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
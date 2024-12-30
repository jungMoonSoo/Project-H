using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SingleSkillArea: SkillAreaBase, ISkillArea
{
    public byte SpriteCode => 0;

    public ITargetingSystem TargetingSystem { get; } = new SingleTargetingSystem();
    

    public void SetPosition(Vector3 worldPosition)
    {
        Unidad[] targets = TargetingSystem.GetTargets(Skill.Caster.Owner, worldPosition, Vector3.one, Vector2.zero);
        if (targets[0] is not null)
        {
            Transform.position = targets[0].transform.position;
        }
        else
        {
            Transform.position = worldPosition;
        }
    }
    public override void SetSize(Vector2 size)
    {
        AreaTransform.localScale = new Vector2(2, 1);
    }
}
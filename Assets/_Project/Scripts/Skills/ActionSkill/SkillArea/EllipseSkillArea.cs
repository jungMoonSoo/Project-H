using UnityEngine;

public class EllipseSkillArea : SkillAreaBase, ISkillArea
{
    public byte SpriteCode => 0;
    
    public void SetPosition(Vector3 worldPosition)
    {
        Transform.position = worldPosition + Vector3.forward;
    }
}

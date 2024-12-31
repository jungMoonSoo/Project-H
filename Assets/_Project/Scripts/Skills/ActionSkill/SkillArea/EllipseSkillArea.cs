using UnityEngine;

public class EllipseSkillArea : ISkillArea
{
    public void SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector2 castedPosition)
    {
        transform.position = castedPosition;
    }
}

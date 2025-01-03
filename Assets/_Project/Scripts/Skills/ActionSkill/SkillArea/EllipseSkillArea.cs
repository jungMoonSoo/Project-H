using UnityEngine;

public class EllipseSkillArea : ISkillArea
{
    public Vector2? SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector2 castedPosition)
    {
        transform.position = VectorCalc.GetPointOnEllipse(caster.skillCollider, castedPosition);
        return transform.position;
    }
}

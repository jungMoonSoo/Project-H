using UnityEngine;

public class EllipseSkillArea : ISkillArea
{
    public Vector3? SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector3 castedPosition)
    {
        transform.position = VectorCalc.GetPointOnEllipse(caster.skillCollider, castedPosition);
        transform.eulerAngles = Vector3.zero;

        return transform.position;
    }
}

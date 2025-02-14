using UnityEngine;

public class GroundSkillArea : ISkillArea
{
    public Vector3? SetPosition(Transform transform, Unidad caster, Vector3 castedPosition)
    {
        transform.position = VectorCalc.GetPointOnEllipse(caster.skillCollider, castedPosition);
        transform.eulerAngles = Vector3.zero;

        return transform.position;
    }
}

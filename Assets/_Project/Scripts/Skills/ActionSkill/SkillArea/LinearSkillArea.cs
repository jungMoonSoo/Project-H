using UnityEngine;

public class LinearSkillArea: ISkillArea
{
    public Vector3? SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector3 castedPosition)
    {
        Vector3 casterPosition = caster.transform.position;
        transform.position = casterPosition;

        float angle = VectorCalc.CalcRotation(castedPosition - casterPosition);
        transform.eulerAngles = new Vector3(0, angle, 0);

        // TODO: 실제 스킬이 날아갈 위치 정해야 함.
        return castedPosition;
    }
}
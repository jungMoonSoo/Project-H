using Unity.VisualScripting;
using UnityEngine;

public class LinearSkillArea: ISkillArea
{
    public Vector2? SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector2 castedPosition)
    {
        Vector2 casterPosition = caster.transform.position;
        transform.position = casterPosition;

        float angle = VectorCalc.CalcRotation(castedPosition - casterPosition);
        transform.eulerAngles = new Vector3(0, 0, angle);

        // TODO: 실제 스킬이 날아갈 위치 정해야 함.
        return castedPosition;
    }
}
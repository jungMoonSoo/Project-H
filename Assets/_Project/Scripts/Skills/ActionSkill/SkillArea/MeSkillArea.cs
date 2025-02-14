using UnityEngine;

public class MeSkillArea: ISkillArea
{
    public Vector3? SetPosition(Transform transform, Unidad caster, Vector3 castedPosition)
    {
        transform.position = caster.transform.position;
        return transform.position;
    }
}
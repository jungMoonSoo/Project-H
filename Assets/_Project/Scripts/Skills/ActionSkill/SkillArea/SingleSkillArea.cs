using System.Collections.Generic;
using UnityEngine;

public class SingleSkillArea: ISkillArea
{
    public Vector3? SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector3 castedPosition)
    {
        Vector3 realPosition = VectorCalc.GetPointOnEllipse(caster.skillCollider, castedPosition);
        List<Unidad> targets = UnidadManager.Instance.GetUnidads(caster.Owner, targetType);
        Unidad target = null;

        if (targets.Count > 0) // 선택될 수 있는 Target이 존재함.
        {
            float minRange = float.MaxValue;

            foreach (Unidad enemy in targets)
            {
                if (!enemy.unitCollider.OnEllipseEnter(caster.skillCollider)) continue;

                Vector3 dir = enemy.transform.position - castedPosition;
                float range = dir.magnitude;

                if (range < minRange)
                {
                    target = enemy;
                    minRange = range;
                }
            }
        }

        if (target is not null) // Target 존재
        {
            transform.position = target.transform.position;
            transform.eulerAngles = Vector3.zero;

            return transform.position;
        }
        else // Target 미존재 
        {
            transform.position = realPosition;
            transform.eulerAngles = Vector3.zero;

            return null;
        }
    }
}
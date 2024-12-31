using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SingleSkillArea: ISkillArea
{
    public void SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector2 castedPosition)
    {
        Vector2 realPosition = VectorCalc.GetPointOnEllipse(caster.skillCollider, castedPosition);
        List<Unidad> targets = caster.Status.skillInfo.GetTargets(caster);
        Unidad target = null;

        if (targets.Count > 0) // 선택될 수 있는 Target이 존재함.
        {
            float minRange = float.MaxValue;

            foreach (Unidad enemy in targets)
            {
                if (!enemy.unitCollider.OnEllipseEnter(caster.skillCollider)) continue;

                Vector2 dir = (Vector2)enemy.transform.position - castedPosition;
                float range = dir.magnitude;

                if (range < minRange)
                {
                    target = enemy;
                    minRange = range;
                }
            }
        }

        if(target is not null) // Target 존재
        {
            transform.position = target.transform.position;
        }
        else // Target 미존재 
        {
            transform.position = realPosition;
        }
    }
}
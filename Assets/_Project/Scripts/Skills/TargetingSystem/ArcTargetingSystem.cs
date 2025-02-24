using System.Collections.Generic;
using UnityEngine;

public class ArcTargetingSystem: IRangeTargeting
{
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, ICustomCollider coll)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner, targetType));

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!coll.OnEnter(targets[i].unitCollider)) targets.Remove(targets[i]);
        }

        return targets.ToArray();
    }

    /// <param name="rangeSize"> x : 범위각, y : 길이 </param>
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner, targetType));

        float castedAngle = GetDirectionToAngle(castedPosition - casterPosition);
        Vector2 range = new(Mathf.Repeat(castedAngle - rangeSize.x * 0.5f, 360), Mathf.Repeat(castedAngle + rangeSize.x * 0.5f, 360));
        Vector2 areaSize = new(rangeSize.y, rangeSize.y);

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!CheckTargetInArea(casterPosition, areaSize, targets[i].unitCollider.Center, targets[i].unitCollider.Radius, range)) targets.Remove(targets[i]);
        }

        return targets.ToArray();
    }

    private bool CheckTargetInArea(Vector3 areaPos, Vector2 areaSize, Vector3 targetPos, Vector2 targetSize, Vector2 range)
    {
        if (VectorCalc.CalcEllipse(areaPos, targetPos, areaSize, targetSize) > 1f) return false;

        foreach (Vector3 point in GetContactPoints(targetPos, targetSize))
        {
            if (CheckDirectionInAngle(GetDirectionToAngle(point - areaPos), range)) return true;
        }

        if (CheckDirectionInAngle(GetDirectionToAngle(targetPos - areaPos), range)) return true;

        return false;
    }

    private bool CheckDirectionInAngle(float angle, Vector2 range)
    {
        if (range.x <= range.y)
        {
            if (angle >= range.x && angle <= range.y) return true;
        }
        else
        {
            if (angle >= range.x || angle <= range.y) return true;
        }

        return false;
    }

    private float GetDirectionToAngle(Vector3 direction) => Mathf.Repeat(Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg, 360);

    private Vector3[] GetContactPoints(Vector3 pos, Vector2 size) => new Vector3[] { pos + new Vector3(-size.x, 0), pos + new Vector3(size.x, 0), pos + new Vector3(0, 0, -size.y), pos + new Vector3(0, 0, size.y) };
}
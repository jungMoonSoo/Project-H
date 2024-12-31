using System.Collections.Generic;
using UnityEngine;

public class ArcTargetingSystem: ITargetingSystem
{
    public Unidad[] GetTargets(UnitType targetOwner, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner));

        float castedAngle = GetDirectionToAngle(castedPosition - casterPosition);
        Vector2 range = new(Mathf.Repeat(castedAngle - rangeSize.y * 0.5f, 360), Mathf.Repeat(castedAngle + rangeSize.y * 0.5f, 360));
        Vector2 areaSize = new(rangeSize.x, rangeSize.x);

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Vector2 targetPos = (Vector2)targets[i].transform.position + targets[i].unitCollider.center;

            if (!CheckTargetInArea(casterPosition, areaSize, targetPos, targets[i].unitCollider.Radius, range)) targets.Remove(targets[i]);
        }

        return targets.ToArray();
    }

    private bool CheckTargetInArea(Vector2 areaPos, Vector2 areaSize, Vector2 targetPos, Vector2 targetSize, Vector2 range)
    {
        if (VectorCalc.CalcEllipse(areaPos, targetPos, areaSize, targetSize) > 1f) return false;

        foreach (Vector2 point in GetContactPoints(targetPos, targetSize))
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

    private float GetDirectionToAngle(Vector2 direction) => Mathf.Repeat(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, 360);

    private Vector2[] GetContactPoints(Vector2 pos, Vector2 size) => new Vector2[] { pos + new Vector2(-size.x, 0), pos + new Vector2(size.x, 0), pos + new Vector2(0, -size.y), pos + new Vector2(0, size.y) };
}
using System.Collections.Generic;
using UnityEngine;

public class LinearTargetingSystem : IRangeTargeting
{
    /// <param name="rangeSize"> x : 너비, y : 길이 </param>
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector3 casterPosition, Vector3 castedPosition, Vector2 rangeSize)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner, targetType));

        Vector3[] points = GetPoints(casterPosition, (castedPosition - casterPosition).normalized, rangeSize);

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!CheckEllipseHitLines(targets[i].unitCollider.Center, targets[i].unitCollider.Radius, points))
            {
                if (!CheckEllipseInBox(points, targets[i].unitCollider.Center)) targets.Remove(targets[i]);
            }
        }

        return targets.ToArray();
    }

    private bool CheckEllipseHitLines(Vector3 pos, Vector2 size, Vector3[] points)
    {
        if (CheckEllipseHitLine(pos, size, points[0], points[1])) return true;
        if (CheckEllipseHitLine(pos, size, points[1], points[2])) return true;
        if (CheckEllipseHitLine(pos, size, points[2], points[3])) return true;
        if (CheckEllipseHitLine(pos, size, points[3], points[0])) return true;

        return false;
    }

    private bool CheckEllipseHitLine(Vector3 pos, Vector2 size, Vector3 pointA, Vector3 pointB)
    {
        Vector3 direction = pointB - pointA;

        float A = (direction.x * direction.x) / (size.x * size.x) + (direction.z * direction.z) / (size.y * size.y);
        float B = 2 * ((pointA.x - pos.x) * direction.x) / (size.x * size.x) + 2 * ((pointA.z - pos.z) * direction.z) / (size.y * size.y);
        float C = ((pointA.x - pos.x) * (pointA.x - pos.x)) / (size.x * size.x) + ((pointA.z - pos.z) * (pointA.z - pos.z)) / (size.y * size.y) - 1;

        float discriminant = B * B - 4 * A * C;

        if (discriminant < 0) return false;

        float t1 = (-B + Mathf.Sqrt(discriminant)) / (2 * A);
        float t2 = (-B - Mathf.Sqrt(discriminant)) / (2 * A);

        return (t1 >= 0f && t1 <= 1f) || (t2 >= 0f && t2 <= 1f);
    }

    private bool CheckEllipseInBox(Vector3[] points, Vector3 targetPos)
    {
        float minX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x);
        float maxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x);
        float minZ = Mathf.Min(points[0].z, points[1].z, points[2].z, points[3].z);
        float maxZ = Mathf.Max(points[0].z, points[1].z, points[2].z, points[3].z);

        return targetPos.x >= minX && targetPos.x <= maxX && targetPos.z >= minZ && targetPos.z <= maxZ;
    }

    private Vector3[] GetPoints(Vector3 pos, Vector3 direction, Vector2 size)
    {
        size.x *= 0.5f;

        Vector3[] corners = new Vector3[]
        {
            pos + RotatePoint(new Vector3(-size.x, 0, 0), direction),
            pos + RotatePoint(new Vector3(-size.x, 0, size.y), direction),
            pos + RotatePoint(new Vector3(size.x, 0, size.y), direction),
            pos + RotatePoint(new Vector3(size.x, 0, 0), direction),
        };

        return corners;
    }

    private Vector3 RotatePoint(Vector3 point, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.z, direction.x);
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        return new Vector3(point.z * cosAngle - point.x * sinAngle, 0, point.z * sinAngle + point.x * cosAngle);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class LinearTargetingSystem : ITargetingSystem
{
    /// <param name="rangeSize"> x : 너비, y : 길이 </param>
    public Unidad[] GetTargets(UnitType targetOwner, TargetType targetType, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        List<Unidad> targets = new(UnidadManager.Instance.GetUnidads(targetOwner, targetType));

        Vector2[] points = GetPoints(casterPosition, (castedPosition - casterPosition).normalized, rangeSize);

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Vector2 targetPos = (Vector2)targets[i].transform.position + targets[i].unitCollider.center;

            if (!CheckEllipseHitLines(targetPos, targets[i].unitCollider.Radius, points))
            {
                if (!CheckEllipseInBox(points, targetPos)) targets.Remove(targets[i]);
            }
        }

        return targets.ToArray();
    }

    private bool CheckEllipseHitLines(Vector2 pos, Vector2 size, Vector2[] points)
    {
        if (CheckEllipseHitLine(pos, size, points[0], points[1])) return true;
        if (CheckEllipseHitLine(pos, size, points[1], points[2])) return true;
        if (CheckEllipseHitLine(pos, size, points[2], points[3])) return true;
        if (CheckEllipseHitLine(pos, size, points[3], points[0])) return true;

        return false;
    }

    private bool CheckEllipseHitLine(Vector2 pos, Vector2 size, Vector2 pointA, Vector2 pointB)
    {
        Vector2 direction = pointB - pointA;

        float A = (direction.x * direction.x) / (size.x * size.x) + (direction.y * direction.y) / (size.y * size.y);
        float B = 2 * ((pointA.x - pos.x) * direction.x) / (size.x * size.x) + 2 * ((pointA.y - pos.y) * direction.y) / (size.y * size.y);
        float C = ((pointA.x - pos.x) * (pointA.x - pos.x)) / (size.x * size.x) + ((pointA.y - pos.y) * (pointA.y - pos.y)) / (size.y * size.y) - 1;

        float discriminant = B * B - 4 * A * C;

        if (discriminant < 0) return false;

        float t1 = (-B + Mathf.Sqrt(discriminant)) / (2 * A);
        float t2 = (-B - Mathf.Sqrt(discriminant)) / (2 * A);

        return (t1 >= 0f && t1 <= 1f) || (t2 >= 0f && t2 <= 1f);
    }

    private bool CheckEllipseInBox(Vector2[] points, Vector2 targetPos)
    {
        float minX = Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x);
        float maxX = Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x);
        float minY = Mathf.Min(points[0].y, points[1].y, points[2].y, points[3].y);
        float maxY = Mathf.Max(points[0].y, points[1].y, points[2].y, points[3].y);

        return targetPos.x >= minX && targetPos.x <= maxX && targetPos.y >= minY && targetPos.y <= maxY;
    }

    private Vector2[] GetPoints(Vector2 pos, Vector2 direction, Vector2 size)
    {
        size.x *= 0.5f;

        Vector2[] corners = new Vector2[4]
        {
            pos + RotatePoint(new Vector2(-size.x, 0), direction),
            pos + RotatePoint(new Vector2(-size.x, size.y), direction),
            pos + RotatePoint(new Vector2(size.x, size.y), direction),
            pos + RotatePoint(new Vector2(size.x, 0), direction),
        };

        return corners;
    }

    private Vector2 RotatePoint(Vector2 point, Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        return new Vector2( point.y * cosAngle - point.x * sinAngle, point.y * sinAngle + point.x * cosAngle );
    }
}
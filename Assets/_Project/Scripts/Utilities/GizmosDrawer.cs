#if UNITY_EDITOR
using UnityEngine;

public static class GizmosDrawer
{
    public static void DrawEllipse(Vector3 center, float size, int angleCount)
    {
        DrawEllipse(center, size, angleCount, Color.white);
    }

    public static void DrawEllipse(Vector3 center, float size, int angleCount, Color color)
    {
        DrawEllipse(center, new Vector2(size, size), angleCount, color);
    }

    public static void DrawEllipse(Vector3 center, Vector2 size, int angleCount)
    {
        DrawEllipse(center, size, angleCount, Color.white);
    }

    public static void DrawEllipse(Vector3 center, Vector2 size, int angleCount, Color color)
    {
        Vector3 previousPoint = GetEllipsePoint(center, 0, size);
        Vector3 currentPoint;

        Gizmos.color = color;

        for (int i = 1; i <= angleCount; i++)
        {
            currentPoint = GetEllipsePoint(center, i * Mathf.PI * 2 / angleCount, size);
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
    }

    public static void DrawBox(Vector3[] points, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[1], points[2]);
        Gizmos.DrawLine(points[2], points[3]);
        Gizmos.DrawLine(points[3], points[0]);
    }

    public static void DrawArc(Vector3 center, Vector2 size, Vector2 range, int angleCount, Color color)
    {
        Gizmos.color = color;

        Vector3 pos1 = VectorCalc.GetPointOnEllipse(center, size, Quaternion.Euler(0, range.x, 0) * Vector3.forward, true);
        Vector3 pos2 = VectorCalc.GetPointOnEllipse(center, size, Quaternion.Euler(0, range.y, 0) * Vector3.forward, true);

        Gizmos.DrawLine(center, pos1);
        Gizmos.DrawLine(center, pos2);

        float rangeCalc = Mathf.Repeat(range.y - range.x, 360) / angleCount;

        for (int i = 1; i <= angleCount; i++)
        {
            pos2 = VectorCalc.GetPointOnEllipse(center, size, Quaternion.Euler(0, range.x + rangeCalc * i, 0) * Vector3.forward, true);

            Gizmos.DrawLine(pos1, pos2);

            pos1 = pos2;
        }
    }

    private static Vector3 GetEllipsePoint(Vector3 center, float angleIdx, Vector2 size)
    {
        return center + new Vector3(Mathf.Sin(angleIdx) * size.x, 0, Mathf.Cos(angleIdx) * size.y);
    }
}
#endif
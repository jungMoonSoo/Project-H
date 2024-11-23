using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosDrawer
{
    public static void DrawEllipse(Vector2 center, float size, int angleCount)
    {
        DrawEllipse(center, size, angleCount, Color.white);
    }
    public static void DrawEllipse(Vector2 center, float size, int angleCount, Color color)
    {
        DrawEllipse(center, new Vector2(size, size), angleCount, color);
    }
    public static void DrawEllipse(Vector2 center, Vector2 size, int angleCount)
    {
        DrawEllipse(center, size, angleCount, Color.white);
    }
    public static void DrawEllipse(Vector2 center, Vector2 size, int angleCount, Color color)
    {
#if UNITY_EDITOR
        Vector2 previousPoint = GetEllipsePoint(center, 0, size);
        Vector2 currentPoint;

        Gizmos.color = color;
        for (int i = 1; i <= angleCount; i++)
        {
            currentPoint = GetEllipsePoint(center, i * Mathf.PI * 2 / angleCount, size);
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
    }

    private static Vector2 GetEllipsePoint(Vector2 center, float angleIdx, Vector2 size)
    {
        return center + new Vector2(Mathf.Sin(angleIdx) * size.x, Mathf.Cos(angleIdx) * size.y);
#endif
    }
}

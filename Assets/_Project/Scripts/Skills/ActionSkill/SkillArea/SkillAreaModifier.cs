using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillAreaModifier
{
    public static Vector2 GetPointOnEllipse(EllipseCollider ellipseCollider, Vector2 point)
    {
        Vector2 colliderSize = ellipseCollider.size * 0.5f;
        Vector2 colliderPos = (Vector2)ellipseCollider.transform.position + ellipseCollider.center;
        Vector2 dir = point - colliderPos;

        float normalizedX = dir.x / colliderSize.x;
        float normalizedY = dir.y / colliderSize.y;

        float distanceSquared = normalizedX * normalizedX + normalizedY * normalizedY;

        if (distanceSquared <= 1f) return point;

        return colliderPos + new Vector2(normalizedX / Mathf.Sqrt(distanceSquared), normalizedY / Mathf.Sqrt(distanceSquared)) * colliderSize;
    }
}

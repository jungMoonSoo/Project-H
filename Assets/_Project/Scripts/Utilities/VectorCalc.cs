using UnityEngine;

public static class VectorCalc
{
    public static float CalcRotation(Vector3 distance)
    {
        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }

    public static float CalcEllipse(Vector2 center, Vector2 target, Vector2 centerRadius, Vector2 targetRadius) =>
        CalcEllipse(target - center, centerRadius, targetRadius);
    public static float CalcEllipse(Vector2 direction, Vector2 centerRadius, Vector2 targetRadius)
    {
        return Mathf.Pow(direction.x / (centerRadius.x + targetRadius.x), 2) +
               Mathf.Pow(direction.y / (centerRadius.y + targetRadius.y), 2);
    }
}
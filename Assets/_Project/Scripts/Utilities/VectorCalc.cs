using UnityEngine;

public static class VectorCalc
{
    public static float CalcRotation(Vector3 distance)
    {
        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }
}
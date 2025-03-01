using UnityEngine;

public static class Teeeest
{
    public static Vector3 GetEvasiveVector(Unidad current, Unidad target)
    {
        Vector3 dir = target.unitCollider.Center - current.unitCollider.Center;

        float targetDist = dir.magnitude;
        Vector3 targetDir = dir.normalized;

        foreach (Unidad obstacle in UnidadManager.Instance.GetUnidads(current.Owner, TargetType.We))
        {
            if (obstacle == current) continue;

            dir += GetReflectVector(current, obstacle, targetDist, targetDir);
        }

        foreach (Unidad obstacle in UnidadManager.Instance.GetUnidads(current.Owner, TargetType.They))
        {
            if (obstacle == target) continue;

            dir += GetReflectVector(current, obstacle, targetDist, targetDir);
        }

        return current.unitCollider.Center + dir.normalized * targetDist;
    }

    private static Vector3 GetReflectVector(Unidad current, Unidad obstacle, float targetDist, Vector3 targetDir)
    {
        Vector3 obstacleDir = obstacle.unitCollider.Center - current.unitCollider.Center;
        float dot = Vector3.Dot(obstacleDir, targetDir);

        if (dot > 0)
        {
            Vector3 projectiondir = targetDir * (dot * targetDir).magnitude;

            if (VectorCalc.CalcEllipse(current.unitCollider.Center, obstacle.unitCollider.Center - projectiondir, current.unitCollider.Radius, obstacle.unitCollider.Radius) <= 1f)
            {
                return (targetDist - obstacleDir.magnitude) * 2 * -obstacleDir.normalized;
            }
        }

        return Vector3.zero;
    }
}

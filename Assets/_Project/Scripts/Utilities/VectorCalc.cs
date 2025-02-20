using System.Drawing;
using UnityEngine;

public static class VectorCalc
{
    /// <summary>
    /// 상대 거리를 오일러 각도로 바꿔주는 Method
    /// </summary>
    /// <param name="distance">상대 거리</param>
    /// <returns>오일러 각도</returns>
    public static float CalcRotation(Vector3 distance)
    {
        return Mathf.Atan2(distance.z, distance.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 타원방정식에서 특정 지점이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="center">타원의 중심 위치</param>
    /// <param name="centerRadius">타원의 반지름</param>
    /// <param name="point">특정 지점</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipsePoint(Vector3 center, Vector2 centerRadius, Vector3 point) =>
        CalcEllipse(center, point, centerRadius, Vector2.zero);
    
    /// <summary>
    /// 타원방정식에서 두 타원이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="center">A타원의 중심 위치</param>
    /// <param name="target">B타원의 중심 위치</param>
    /// <param name="centerRadius">A타원의 반지름</param>
    /// <param name="targetRadius">B타원의 반지름</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipse(Vector3 center, Vector3 target, Vector2 centerRadius, Vector2 targetRadius) =>
        CalcEllipse(target - center, centerRadius, targetRadius);

    /// <summary>
    /// 타원방정식에서 두 타원이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="direction">두 타원의 상대 거리</param>
    /// <param name="centerRadius">A타원의 반지름</param>
    /// <param name="targetRadius">B타원의 반지름</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipse(Vector3 direction, Vector2 centerRadius, Vector2 targetRadius)
    {
        return Mathf.Pow(direction.x / (centerRadius.x + targetRadius.x), 2) +
               Mathf.Pow(direction.z / (centerRadius.y + targetRadius.y), 2);
    }

    /// <summary>
    /// 타원방정식에서 B타원이 A타원 내부에 있는지 반환하는 Method
    /// </summary>
    /// <param name="center">A타원의 중심 위치</param>
    /// <param name="target">B타원의 중심 위치</param>
    /// <param name="centerRadius">A타원의 반지름</param>
    /// <param name="targetRadius">B타원의 반지름</param>
    public static bool CalcInsideEllipse(Vector3 center, Vector3 target, Vector2 centerRadius, Vector2 targetRadius)
    {
        if (CalcEllipse(target - center, centerRadius, targetRadius) > 1) return false;

        if (CalcEllipsePoint(center, centerRadius, target + Vector3.right * targetRadius.x) > 1) return false;
        if (CalcEllipsePoint(center, centerRadius, target + Vector3.left * targetRadius.x) > 1) return false;
        if (CalcEllipsePoint(center, centerRadius, target + Vector3.forward * targetRadius.y) > 1) return false;
        if (CalcEllipsePoint(center, centerRadius, target + Vector3.back * targetRadius.y) > 1) return false;

        return true;
    }

    /// <summary>
    /// 특정 지점이 타원 밖으로 나가면, 최대 위치로 반환하는 Method
    /// </summary>
    /// <param name="ellipseCollider">타원 충돌체</param>
    /// <param name="point">특정 지점</param>
    /// <returns>특정 지점 조정 값</returns>
    public static Vector3 GetPointOnEllipse(EllipseCollider ellipseCollider, Vector3 point) =>
        GetPointOnEllipse(ellipseCollider.transform.position + ellipseCollider.Center, ellipseCollider.size, point);

    /// <summary>
    /// 특정 지점이 타원 밖으로 나가면, 최대 위치로 반환하는 Method
    /// </summary>
    /// <param name="ellipsePosition">타원 위치</param>
    /// <param name="ellipseSize">타원 크기</param>
    /// <param name="point">특정 지점</param>
    /// <returns>수정된 특정 지점의 위치</returns>
    public static Vector3 GetPointOnEllipse(Vector3 ellipsePosition, Vector2 ellipseSize, Vector3 point)
    {
        Vector2 colliderRadius = ellipseSize * 0.5f;
        Vector3 dir = point - ellipsePosition;

        float normalizedX = dir.x / colliderRadius.x;
        float normalizedZ = dir.z / colliderRadius.y;

        float distanceSquared = normalizedX * normalizedX + normalizedZ * normalizedZ;

        if (distanceSquared <= 1f) return point;

        distanceSquared = Mathf.Sqrt(distanceSquared);

        return ellipsePosition + new Vector3(normalizedX * colliderRadius.x, 0, normalizedZ * colliderRadius.y) / distanceSquared;
    }

    public static Vector3 GetRandomPositionInBoxCollider(Vector3 size, Vector2 pivot, Vector2 border)
    {
        float range_X = size.x * 0.5f;
        float range_Y = size.y * 0.5f;

        pivot += Vector2.one;

        range_X = Random.Range((-range_X + border.x) * pivot.x, (range_X - border.x) * (2 - pivot.x));
        range_Y = Random.Range((-range_Y + border.y) * pivot.y, (range_Y - border.y) * (2 - pivot.y));

        return new(range_X, range_Y, size.z * 0.5f);
    }
}
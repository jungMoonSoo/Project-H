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
        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 타원방정식에서 특정 지점이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="center">타원의 중심 위치</param>
    /// <param name="centerRadius">타원의 반지름</param>
    /// <param name="point">특정 지점</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipsePoint(Vector2 center, Vector2 centerRadius, Vector2 point) =>
        CalcEllipse(center, point, centerRadius, Vector2.zero);
    
    /// <summary>
    /// 타원방정식에서 두 타원이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="center">A타원의 중심 위치</param>
    /// <param name="target">B타원의 중심 위치</param>
    /// <param name="centerRadius">A타원의 반지름</param>
    /// <param name="targetRadius">B타원의 반지름</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipse(Vector2 center, Vector2 target, Vector2 centerRadius, Vector2 targetRadius) =>
        CalcEllipse(target - center, centerRadius, targetRadius);
    /// <summary>
    /// 타원방정식에서 두 타원이 접해있는지 계산하는 Method
    /// </summary>
    /// <param name="direction">두 타원의 상대 거리</param>
    /// <param name="centerRadius">A타원의 반지름</param>
    /// <param name="targetRadius">B타원의 반지름</param>
    /// <returns>타원방정식 결과 값, 1이하면 접촉상태</returns>
    public static float CalcEllipse(Vector2 direction, Vector2 centerRadius, Vector2 targetRadius)
    {
        return Mathf.Pow(direction.x / (centerRadius.x + targetRadius.x), 2) +
               Mathf.Pow(direction.y / (centerRadius.y + targetRadius.y), 2);
    }

    /// <summary>
    /// 특정 지점이 타원 밖으로 나가면, 최대 위치로 반환하는 Method
    /// </summary>
    /// <param name="ellipseCollider">타원 충돌체</param>
    /// <param name="point">특정 지점</param>
    /// <returns>특정 지점 조정 값</returns>
    public static Vector2 GetPointOnEllipse(EllipseCollider ellipseCollider, Vector2 point) =>
        GetPointOnEllipse((Vector2)ellipseCollider.transform.position + ellipseCollider.center, ellipseCollider.size, point);
    /// <summary>
    /// 특정 지점이 타원 밖으로 나가면, 최대 위치로 반환하는 Method
    /// </summary>
    /// <param name="ellipsePosition">타원 위치</param>
    /// <param name="ellipseSize">타원 크기</param>
    /// <param name="point">특정 지점</param>
    /// <returns>수정된 특정 지점의 위치</returns>
    public static Vector2 GetPointOnEllipse(Vector2 ellipsePosition, Vector2 ellipseSize, Vector2 point)
    {
        Vector2 colliderRadius = ellipseSize * 0.5f;
        Vector2 dir = point - ellipsePosition;

        float normalizedX = dir.x / colliderRadius.x;
        float normalizedY = dir.y / colliderRadius.y;

        float distanceSquared = normalizedX * normalizedX + normalizedY * normalizedY;

        if (distanceSquared <= 1f) return point;

        return ellipsePosition + new Vector2(normalizedX / Mathf.Sqrt(distanceSquared), normalizedY / Mathf.Sqrt(distanceSquared)) * colliderRadius;
    }

    public static Vector2 GetRandomPositionInBoxCollider(Vector2 size) => new(Random.Range(0, size.x), Random.Range(0, size.y));
}
using UnityEngine;

public class LinearCollider : MonoBehaviour, ICustomCollider
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;

    [Header("Collider Settings")]
    [SerializeField] public Vector3 direction;
    [SerializeField] public Vector2 size = new(2, 1);

    public Vector2 Radius => new(size.x * 0.5f, size.y);
    public Vector3 Center => transform.position;

    public Vector3[] Points => new Vector3[]
    {
        Center + RotatePoint(new Vector3(-Radius.x, 0, 0)),
        Center + RotatePoint(new Vector3(-Radius.x, 0, Radius.y)),
        Center + RotatePoint(new Vector3(Radius.x, 0, Radius.y)),
        Center + RotatePoint(new Vector3(Radius.x, 0, 0)),
    };

    public bool OnEnter(ICustomCollider coll) => CheckEllipseHit(coll.Center, coll.Radius);

    private bool CheckEllipseHit(Vector3 pos, Vector2 size)
    {
        Vector3[] points = Points;

        if (CheckEllipseHitLine(pos, size, points[0], points[1])) return true;
        if (CheckEllipseHitLine(pos, size, points[1], points[2])) return true;
        if (CheckEllipseHitLine(pos, size, points[2], points[3])) return true;
        if (CheckEllipseHitLine(pos, size, points[3], points[0])) return true;

        if (pos.x >= Mathf.Min(points[0].x, points[1].x, points[2].x, points[3].x) &&
            pos.x <= Mathf.Max(points[0].x, points[1].x, points[2].x, points[3].x) &&
            pos.z >= Mathf.Min(points[0].z, points[1].z, points[2].z, points[3].z) &&
            pos.z <= Mathf.Max(points[0].z, points[1].z, points[2].z, points[3].z)) return true;

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

    private Vector3 RotatePoint(Vector3 point)
    {
        float angle = Mathf.Atan2(direction.z, direction.x);
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        return new Vector3(point.z * cosAngle - point.x * sinAngle, 0, point.z * sinAngle + point.x * cosAngle);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawBox(Points, lineColor);
    }
#endif
}

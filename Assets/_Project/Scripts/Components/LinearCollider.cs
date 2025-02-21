using UnityEngine;

public class LinearCollider : MonoBehaviour
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;

    [Header("Collider Settings")]
    [SerializeField] public Vector3 direction;
    [SerializeField] public Vector2 size = new(2, 1);

    public Vector2 Radius => new (size.x * 0.5f, size.y);

    public Vector3 Center => transform.position;

    public Vector3[] Points => new Vector3[]
    {
        Center + RotatePoint(new Vector3(-Radius.x, 0, 0)),
        Center + RotatePoint(new Vector3(-Radius.x, 0, Radius.y)),
        Center + RotatePoint(new Vector3(Radius.x, 0, Radius.y)),
        Center + RotatePoint(new Vector3(Radius.x, 0, 0)),
    };

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

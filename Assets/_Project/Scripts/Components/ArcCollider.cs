using UnityEngine;

public class ArcCollider : MonoBehaviour, ICustomCollider
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;

    [Header("Collider Settings")]
    [SerializeField] public Vector2 size = new(2, 1);

    public Vector2 Radius => new(size.y, size.y);
    public Vector3 Center => transform.position;

    public Vector3 Direction { get; set; }

    private Vector2 Range
    {
        get
        {
            float dirAngle = GetDirectionToAngle(Direction);

            return new(Mathf.Repeat(dirAngle - size.x * 0.5f, 360), Mathf.Repeat(dirAngle + size.x * 0.5f, 360));
        }
    }

    public bool OnEnter(ICustomCollider coll) => CheckTargetInArea(Center, Radius, coll.Center, coll.Radius, Range);

    private bool CheckTargetInArea(Vector3 areaPos, Vector2 areaSize, Vector3 targetPos, Vector2 targetSize, Vector2 range)
    {
        if (VectorCalc.CalcEllipse(areaPos, targetPos, areaSize, targetSize) > 1f) return false;

        foreach (Vector3 point in GetContactPoints(targetPos, targetSize))
        {
            if (CheckDirectionInAngle(GetDirectionToAngle(point - areaPos), range)) return true;
        }

        if (CheckDirectionInAngle(GetDirectionToAngle(targetPos - areaPos), range)) return true;

        return false;
    }

    private bool CheckDirectionInAngle(float angle, Vector2 range)
    {
        if (range.x <= range.y)
        {
            if (angle >= range.x && angle <= range.y) return true;
        }
        else
        {
            if (angle >= range.x || angle <= range.y) return true;
        }

        return false;
    }

    private float GetDirectionToAngle(Vector3 direction) => Mathf.Repeat(Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg, 360);

    private Vector3[] GetContactPoints(Vector3 pos, Vector2 size) => new Vector3[]
    {
        pos + new Vector3(-size.x, 0),
        pos + new Vector3(size.x, 0),
        pos + new Vector3(0, 0, -size.y),
        pos + new Vector3(0, 0, size.y)
    };

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawArc(Center, Radius, Range, (int)(size.x / 6), lineColor);
    }
#endif
}

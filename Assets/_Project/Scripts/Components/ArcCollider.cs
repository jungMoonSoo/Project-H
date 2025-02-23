using UnityEngine;

public class ArcCollider : MonoBehaviour
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;

    [Header("Collider Settings")]
    [SerializeField] public Vector3 direction;
    [SerializeField] public Vector2 size = new(2, 1);

    public float Length => size.x;

    public Vector2 Radius => new Vector2(size.y, size.y) * 0.5f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawArc(transform.position, Radius, Length, direction - transform.position, (int)(Length / 6), lineColor);
    }
#endif
}

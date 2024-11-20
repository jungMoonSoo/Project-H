using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEllipseCollider : MonoBehaviour
{
    [SerializeField] public Vector2 Center;
    [SerializeField] public Vector2 Size;

    /// <summary>
    /// 충돌체 반지름
    /// </summary>
    public Vector2 Radius => Size * 0.5f;

    /// <summary>
    /// Target과의 충돌 확인
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>충돌 여부</returns>
    public bool OnEllipseEnter(NewEllipseCollider target)
    {
        Vector3 dirPos = target.transform.position - (transform.position + (Vector3)Center);

        return Mathf.Pow(dirPos.x / (Radius.x + target.Radius.x), 2) + Mathf.Pow(dirPos.y / (Radius.y + target.Radius.y), 2) <= 1;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawEllipse(transform.position + (Vector3)Center, Radius, 50, Color.green);
    }
#endif
}
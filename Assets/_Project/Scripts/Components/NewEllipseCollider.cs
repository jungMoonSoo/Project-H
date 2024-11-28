using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEllipseCollider : MonoBehaviour
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;
    
    [Header("Collider Settings")]
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
        return OnEllipseDepth(target) <= 1;
    }
    
    /// <summary>
    /// Target과의 타원방정식 계산
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>타원방정식 결과, 1이 접촉, 이하는 </returns>
    public float OnEllipseDepth(NewEllipseCollider target)
    {
        Vector3 dirPos = target.transform.position - (transform.position + (Vector3)Center);

        return Mathf.Pow(dirPos.x / (Radius.x + target.Radius.x), 2) +
               Mathf.Pow(dirPos.y / (Radius.y + target.Radius.y), 2);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawEllipse(transform.position + (Vector3)Center, Radius, 50, lineColor);
    }
#endif
}
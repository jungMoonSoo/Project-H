using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseCollider : MonoBehaviour
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;
    
    [Header("Collider Settings")]
    [SerializeField] public Vector2 center;
    [SerializeField] public Vector2 size = new(2, 1);

    /// <summary>
    /// 충돌체 반지름
    /// </summary>
    public Vector2 Radius => size * 0.5f;

    /// <summary>
    /// Target과의 충돌 확인
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>충돌 여부</returns>
    public bool OnEllipseEnter(EllipseCollider target)
    {
        return OnEllipseDepth(target) <= 1;
    }
    
    /// <summary>
    /// Target과의 타원방정식 계산
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>타원방정식 결과, 1이 접촉, 이하는 </returns>
    public float OnEllipseDepth(EllipseCollider target)
    {
        return VectorCalc.CalcEllipse(transform.position, target.transform.position, Radius, target.Radius);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawEllipse(transform.position + (Vector3)center, Radius, 50, lineColor);
    }
#endif
}
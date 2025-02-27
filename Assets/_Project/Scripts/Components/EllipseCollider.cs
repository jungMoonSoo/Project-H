using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseCollider : MonoBehaviour, ICustomCollider
{
    [Header("Gizmos Settings")]
    [SerializeField] public Color lineColor;
    
    [Header("Collider Settings")]
    [SerializeField] public Vector2 size = new(2, 1);

    public Vector2 AreaSize => size;

    /// <summary>
    /// 충돌체 반지름
    /// </summary>
    public Vector2 Radius => size * 0.5f;
    public Vector3 Center => transform.position;

    public Vector3 Direction { get; set; }

    /// <summary>
    /// Target과의 충돌 확인
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>충돌 여부</returns>
    public bool OnEnter(ICustomCollider target)
    {
        return OnEllipseDepth(target) <= 1;
    }
    
    /// <summary>
    /// Target과의 타원방정식 계산
    /// </summary>
    /// <param name="target">Target</param>
    /// <returns>타원방정식 결과, 1이 접촉, 이하는 </returns>
    public float OnEllipseDepth(ICustomCollider target)
    {
        return VectorCalc.CalcEllipse(Center, target.Center, Radius, target.Radius);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GizmosDrawer.DrawEllipse(Center, Radius, 50, lineColor);
    }
#endif
}
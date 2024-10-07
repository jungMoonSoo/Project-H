using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseCollider : MonoBehaviour
{
    public float attackRange;
    public float unitSize;
    public float heightRatio;

    private Unit unit;

    private void Start()
    {
        TryGetComponent(out unit);
    }

    public Vector3 AroundTarget(EllipseCollider _target, Vector3 _movePos)
    {
        if (transform.position == _target.transform.position) return Random.Range(0, unit.moveSpeed) * Vector3.up;

        Vector3 _rightTurn = Quaternion.Euler(0, 0, -90) * _movePos;
        Vector3 _leftTurn = Quaternion.Euler(0, 0, 90) * _movePos;

        if (OnHitEllipse(transform.position + _rightTurn, _target) > 1) return _rightTurn;
        else if (OnHitEllipse(transform.position + _leftTurn, _target) > 1) return _leftTurn;

        _movePos += unit.moveSpeed * (transform.position - _target.transform.position).normalized;

        return AroundTarget(_target, _movePos);
    }

    public float OnHitEllipse(Vector3 _pos, EllipseCollider _target)
    {
        Vector2 delta = _target.transform.position - _pos;

        return Mathf.Pow(delta.x / (unitSize + _target.unitSize), 2) + Mathf.Pow(delta.y / (unitSize * heightRatio + _target.unitSize * _target.heightRatio), 2);
    }

    public float OnAttackEllipse(Vector3 _pos, EllipseCollider _target)
    {
        Vector2 delta = _target.transform.position - _pos;

        return Mathf.Pow(delta.x / (attackRange + _target.unitSize), 2) + Mathf.Pow(delta.y / (attackRange * heightRatio + _target.unitSize * _target.heightRatio), 2);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawEllipse(unitSize, unitSize * heightRatio, 50);

        Gizmos.color = Color.red;
        DrawEllipse(attackRange, attackRange * heightRatio, 50);
    }

    private void DrawEllipse(float radiusX, float radiusY, int segments)
    {
        Vector3 previousPoint = GetEllipsePoint(0, radiusX, radiusY);

        for (int i = 1; i <= segments; i++)
        {
            Vector3 currentPoint = GetEllipsePoint(i * Mathf.PI * 2 / segments, radiusX, radiusY);

            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
    }

    private Vector3 GetEllipsePoint(float angle, float radiusX, float radiusY)
    {
        return transform.position + new Vector3(Mathf.Cos(angle) * radiusX, Mathf.Sin(angle) * radiusY, 0);
    }
#endif
}

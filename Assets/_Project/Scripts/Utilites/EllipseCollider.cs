using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseCollider : MonoBehaviour
{
    public List<Vector2> ranges = new();

    private Vector2 areaPos;
    private Vector2 areaSize;

    /// <summary>
    /// 충돌체의 활동 영역 설정, TransAreaPos 메서드 호출시
    /// 범위를 벗어난 충돌체의 경우 범위 내부로 이동하는 벡터 반환
    /// </summary>
    /// <param name="_areaPos">중심 좌표</param>
    /// <param name="_areaSize">크기</param>
    public void SetArea(Vector2 _areaPos, Vector2 _areaSize)
    {
        areaPos = _areaPos;
        areaSize = _areaSize;
    }

    /// <summary>
    /// 특정 타겟과 충돌하지 않는 진행 방향에 수직이 되는 벡터 반환
    /// </summary>
    /// <param name="_pos">진행 방향</param>
    /// <param name="_target">회피할 타겟</param>
    /// <param name="_speed">이동 속도</param>
    public Vector3 AroundTarget(Vector3 _pos, EllipseCollider _target, float _speed)
    {
        if (transform.position == _target.transform.position) return TransAreaPos(Random.Range(0, _speed) * Vector3.up);

        Vector3 _rightTurn = TransAreaPos(Quaternion.Euler(0, 0, -90) * _pos);
        Vector3 _leftTurn = TransAreaPos(Quaternion.Euler(0, 0, 90) * _pos);

        if (OnEllipseEnter(transform.position + _rightTurn, _target, EllipseType.Unit, EllipseType.Unit) > 1) return _rightTurn;
        if (OnEllipseEnter(transform.position + _leftTurn, _target, EllipseType.Unit, EllipseType.Unit) > 1) return _leftTurn;

        return TransAreaPos(_pos + _speed * (transform.position - _target.transform.position).normalized);
    }

    /// <summary>
    /// 특정 타겟과의 거리 반환, 1보다 작거나 같은 경우 충돌중
    /// EllipseType는 상단에 선언 되어있는 ranges의 인덱스입니다.
    /// </summary>
    /// <param name="_pos">충돌체의 중심 좌표</param>
    /// <param name="_target">검사할 타겟</param>
    /// <param name="_num">충돌체 타입</param>
    /// <param name="_targetNum">타겟의 충돌체 타입</param>
    public float OnEllipseEnter(Vector3 _pos, EllipseCollider _target, EllipseType _num, EllipseType _targetNum)
    {
        _pos = _target.transform.position - _pos;

        return Mathf.Pow(_pos.x / (ranges[(int)_num].x + _target.ranges[(int)_targetNum].x), 2) + Mathf.Pow(_pos.y / (ranges[(int)_num].y + _target.ranges[(int)_targetNum].y), 2);
    }

    /// <summary>
    /// 범위를 벗어난 좌표를 범위 내부의 좌표로 변환
    /// </summary>
    /// <param name="_pos">확인할 좌표</param>
    public Vector2 TransAreaPos(Vector3 _pos)
    {
        float _dist = (areaPos.x - areaSize.x * 0.5f) - (transform.position.x + _pos.x - ranges[0].x);

        if (_dist > 0) _pos.x += _dist;

        _dist = (areaPos.x + areaSize.x * 0.5f) - (transform.position.x + _pos.x + ranges[0].x);

        if (_dist < 0) _pos.x += _dist;

        _dist = (areaPos.y - areaSize.y * 0.5f) - (transform.position.y + _pos.y - ranges[0].y);

        if (_dist > 0) _pos.y += _dist;

        _dist = (areaPos.y + areaSize.y * 0.5f) - (transform.position.y + _pos.y + ranges[0].y);

        if (_dist < 0) _pos.y += _dist;

        return _pos;
    }

#if UNITY_EDITOR

    private readonly List<Color> colors = new() { Color.green, Color.red, Color.gray };

    private void OnDrawGizmos()
    {
        for (int i = 0; i < ranges.Count; i++)
        {
            Gizmos.color = colors[i];
            GizmosDrawer.DrawEllipse(transform.position, ranges[i], 50, colors[i]);
        }
    }
#endif
}

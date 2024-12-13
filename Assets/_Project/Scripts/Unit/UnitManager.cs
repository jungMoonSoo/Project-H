using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    public bool isPlay;

    public Vector2 mapPos;
    public Vector2 mapSize;

    public readonly LerpAction lerpAction = new();

    private void FixedUpdate()
    {
        lerpAction.actions?.Invoke();
    }

    public void End()
    {
        if (!isPlay) return;

        isPlay = false;

        // for (int i = 0; i < units.Count; i++) units[i].ReturnToPos();

        Debug.Log("End");
    }

    public Vector2 TransArea(Vector3 _pos, Vector2 _colliderSize)
    {
        float _dist = (mapPos.x - mapSize.x * 0.5f) - (_pos.x - _colliderSize.x);

        if (_dist > 0) _pos.x += _dist;

        _dist = (mapPos.x + mapSize.x * 0.5f) - (_pos.x + _colliderSize.x);

        if (_dist < 0) _pos.x += _dist;

        _dist = (mapPos.y - mapSize.y * 0.5f) - (_pos.y - _colliderSize.y);

        if (_dist > 0) _pos.y += _dist;

        _dist = (mapPos.y + mapSize.y * 0.5f) - (_pos.y + _colliderSize.y);

        if (_dist < 0) _pos.y += _dist;

        return _pos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        // 직사각형의 중심과 크기를 기반으로 기즈모 그리기
        Vector3 bottomLeft = mapPos + new Vector2(-mapSize.x / 2, -mapSize.y / 2);
        Vector3 topRight = mapPos + new Vector2(mapSize.x / 2, mapSize.y / 2);

        Gizmos.DrawLine(bottomLeft, new Vector2(bottomLeft.x, topRight.y)); // 왼쪽 선
        Gizmos.DrawLine(bottomLeft, new Vector2(topRight.x, bottomLeft.y)); // 아래 선
        Gizmos.DrawLine(new Vector2(topRight.x, bottomLeft.y), topRight); // 오른쪽 선
        Gizmos.DrawLine(new Vector2(bottomLeft.x, topRight.y), topRight); // 위쪽 선
    }
#endif
}

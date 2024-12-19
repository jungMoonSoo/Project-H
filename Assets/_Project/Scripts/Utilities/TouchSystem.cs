using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSystem : Singleton<TouchSystem>
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

#if UNITY_EDITOR
    public TouchInfo GetTouch(int _index)
    {
        TouchInfo _info = new();

        if (Input.GetMouseButton(_index))
        {
            _info.phase = Input.GetMouseButtonDown(_index) ? TouchPhase.Began : TouchPhase.Moved;
            _info.count = _index + 1;
        }

        if (_info.count > _index)
        {
            _info.pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D _hit = Physics2D.Raycast(_info.pos, Vector2.zero);

            if (_hit.collider is not null) _info.gameObject = _hit.collider.gameObject;
        }
        else _info.phase = TouchPhase.Ended;

        return _info;
    }
#else
    public TouchInfo GetTouch(int _index)
    {
        TouchInfo _info = new() { count = Input.touchCount };

        if (_info.count > _index)
        {
            Touch _touch = Input.GetTouch(_index);

            _info.phase = _touch.phase;
            _info.pos = mainCamera.ScreenToWorldPoint(_touch.position);

            RaycastHit2D _hit = Physics2D.Raycast(_info.pos, Vector2.zero);

            if (_hit.collider != null) _info.gameObject = _hit.collider.gameObject;
        }

        return _info;
    }
#endif
}

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
}

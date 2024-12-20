using UnityEngine;

public class TouchSystem : Singleton<TouchSystem>
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

#if UNITY_EDITOR
    public TouchInfo GetTouch(int index)
    {
        TouchInfo info = new();

        if (Input.GetMouseButton(index))
        {
            info.phase = Input.GetMouseButtonDown(index) ? TouchPhase.Began : TouchPhase.Moved;
            info.count = index + 1;
        }

        if (info.count > index)
        {
            info.pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(info.pos, Vector2.zero);

            if (hit.collider is not null) info.gameObject = hit.collider.gameObject;
        }
        else info.phase = TouchPhase.Ended;

        return info;
    }
#else
    public TouchInfo GetTouch(int index)
    {
        TouchInfo info = new() { count = Input.touchCount };

        if (info.count > index)
        {
            Touch touch = Input.GetTouch(index);

            info.phase = touch.phase;
            info.pos = mainCamera.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(info.pos, Vector2.zero);

            if (hit.collider != null) info.gameObject = hit.collider.gameObject;
        }

        return info;
    }
#endif
}

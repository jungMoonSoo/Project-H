using UnityEngine;

public class TouchInfo
{
    public int count;
    public int fingerId;
    public TouchPhase phase = TouchPhase.Ended;

    public int length = 0;
    public RaycastHit[] hits = new RaycastHit[5];

    public Vector3 GetPos(int index)
    {
        if (length <= index) return Vector3.zero;

        return hits[index].point;
    }

    public GameObject this[int index]
    {
        get
        {
            if (length <= index) return null;

            if (hits[index].collider == null) return null;
            else return hits[index].collider.gameObject;
        }
    }
}
